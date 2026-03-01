from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from bs4 import BeautifulSoup
import time
import os
import openpyxl


BASE_URL = "https://hcunits.net/units/"
OUTPUT_DIR = "unit_html_selenium"
os.makedirs(OUTPUT_DIR, exist_ok=True)

SET_ABBREVIATIONS = [
    "ffgc", "ve", "d26", "m26", "wk26", "st", "xm97", "d400", "ll", "sd", "spv", "m400", "cltr", "roc25", "rwc25", "wk25", "wkd25", "wkm25", "dndicn", "bp", "d25", "m25", "wbicn", "mot", "dwx", "hgpc2", "msnp", "cco24", "mjx24", "roc24", "wk24", "wkd24", "wkm24", "gotghc", "d24", "m24", "wov", "not", "av60", "dicn", "dicnDCSM","dicnDCX", "micn", "wicn", "smba", "cco23", "mjx23", "roc23", "wk23", "wkd23", "wkm23", "btu", "av4e", "hgpc", "xmxssop", "xmxs", "msdp", "wotr", "ffwotr", "mjx22", "roc22", "wk22", "affe", "ff2021", "em", "xmrf", "ffxmrf", "ww80", "ffff", "ffffff", "dcff", "hx", "ffhx", "mjx21", "roc21", "wk21", "wkd21", "wkm21", "svc", "ffsvc", "bgame", "f4", "fff4", "bwm", "ffcc", "jlu", "caav", "ffcaav", "mjx20", "roc20", "wk20", "wkd20", "wkm20", "dcxm", "trekbg", "orvl", "wwe", "xdps", "ffxdps", "trekrf", "fftng", "wcr", "abpi", "ffabpi", "re", "ffre", "cmm", "eax", "ffeax", "mjx19", "roc19", "wkd19", "wkm19", "swb", "ffswb", "btas", "tmnt4", "fftmnt4", "ai", "ffai", "xxs", "ffxxs", "roc18", "wk18", "wkd18", "wkm18", "trekos", "hq", "ffhq", "trek4", "trm", "tmt", "und", "ew", "wi", "ww", "adw", "ffadw", "gotg2m", "dxf", "ffdxf", "tmnt3", "fftmnt3", "roc17", "wk17", "wkd17", "wkm17", "jw", "ffjw", "tmnt2", "fftmnt2", "sfsm", "ffsfsm", "cwsop", "uxm", "ffuxm", "tmnt", "cacw", "cacws", "bvs", "ffbvs", "wf", "ffwf", "roc16", "wk16", "wkd16", "wkm16", "smww", "ex", "nfaos", "ffnfaos", "aou", "ffoa", "ygo3", "avas", "aqs", "aaou", "jltw", "ffjltw", "botm", "ygo2", "roc15", "wk15", "wkd15", "wkm15", "fl", "fffl", "hbtbfa", "gotg", "ffgotg", "jlsg", "gotgm", "rotk", "wol", "dofp", "dp", "ffdp", "bmqs",
    "catws", "slosh", "fflod", "ygo", "roc14", "wk14", "wkd14", "wkm14",
    "avx", "iim", "bmao", "dota2", "hbtdos", "mkr", "t2t", "hbtjlm", "trek3", "tdw",
    "bctv", "wxm", "lr", "ka2", "pr", "bsi", "fotr", "mos", "im", "fi", "tt", "fftt",
    "tae", "im3", "stmg", "gc", "asm", "stt2", "wk13", "wkd13", "wkm13",
    "sog", "ffsog", "bm", "hbt", "ffbm", "nml", "acb", "acr", "tab", "d10a", "m10a",
    "jl52", "ffjl", "sdcc", "cw", "ffcw", "sttat", "dkr", "ffgsx", "ffwol",
    "avm", "ams", "gg", "ffgga", "trek", "ig", "wk12", "wkd12", "wkm12",
    "lotr", "ih", "ffih", "sm", "ffsm", "halo", "sf", "gow", "ffha", "ffwm", "ca",
    "glgf", "ffgl", "gx", "wkd11", "wkm11",
    "an", "bd", "ws", "wm", "jh", "bn", "bb",
    "cl", "ha", "aa", "si", "ba", "cr", "mu", "jl", "av", "ls", "hb", "or", "2099", "sv", "gl", "iv", "dr", "sn", "gi", "cd", "aw", "cv", "io", "ff", "lg", "mm", "ch", "ul", "ui", "un", "cm", "in", "cj", "xp", "ct", "ht", "ic", "ata", "wk"
]


options = Options()
options.add_argument('--headless')
options.add_argument('--disable-gpu')
options.add_argument('--no-sandbox')
options.add_argument('--window-size=1920,1080')

driver = webdriver.Chrome(options=options)

# Max seconds to wait for the unit card JS to render. Increase if units are missed.
JS_RENDER_TIMEOUT = 5
# Shorter timeout used when probing for a set's entry point (faster failure on missing sets).
JS_PROBE_TIMEOUT = 2
# Courtesy pause between requests (seconds). Keep >= 0.5 to be polite to the server.
COURTESY_DELAY = 0.5


def load_page_with_unit_card(unit_code, timeout=None):
    """Navigate to a unit page and wait for unitCardsContainer. Returns page source or None."""
    if timeout is None:
        timeout = JS_RENDER_TIMEOUT
    url = f"{BASE_URL}{unit_code}/"
    driver.get(url)
    try:
        WebDriverWait(driver, timeout).until(
            EC.presence_of_element_located((By.ID, "unitCardsContainer"))
        )
        time.sleep(COURTESY_DELAY)
        return driver.page_source
    except Exception:
        time.sleep(COURTESY_DELAY)
        return None


def get_unit_ids_from_sidebar(html):
    """Parse the set sidebar and return all unit IDs listed (e.g. 'dicn001', 'dicn001bta')."""
    soup = BeautifulSoup(html, "html.parser")
    unit_ids = []
    for tag in soup.find_all("a", id=lambda x: x and x.startswith("unitListPanelItem_")):
        unit_id = tag["id"].replace("unitListPanelItem_", "")
        unit_ids.append(unit_id)
    return unit_ids


def save_unit(unit_code, html):
    out_path = os.path.join(OUTPUT_DIR, f"{unit_code}.txt")
    with open(out_path, "w", encoding="utf-8") as f:
        f.write(html)
    print(f"  Saved {unit_code}")


# Error tracking workbook
ERROR_FILE = "save_unit_errors.xlsx"
error_wb = openpyxl.Workbook()
error_ws = error_wb.active
error_ws.title = "Errors"
error_ws.append(["Set Abbreviation", "Error Type", "Detail"])

for set_abbr in SET_ABBREVIATIONS:
    print(f"\nProcessing set: {set_abbr}")

    # Step 1: Try the first unit (001) as the entry point to get the sidebar unit list.
    entry_html = load_page_with_unit_card(f"{set_abbr}001", timeout=JS_PROBE_TIMEOUT)
    if not entry_html:
        print(f"  No valid entry found for {set_abbr}, skipping.")
        error_ws.append([set_abbr, "No entry point", f"{set_abbr}001 not found"])
        continue
    print(f"  Found entry point: {set_abbr}001")

    # Step 2: Extract the full unit ID list from the sidebar.
    unit_ids = get_unit_ids_from_sidebar(entry_html)
    if not unit_ids:
        print(f"  No unit IDs found in sidebar for {set_abbr}, skipping.")
        error_ws.append([set_abbr, "No sidebar IDs", f"Sidebar empty for {set_abbr}001"])
        continue
    print(f"  Found {len(unit_ids)} unit(s) in sidebar.")

    # Step 3: Fetch and save each unit, skipping ones already downloaded.
    for unit_id in unit_ids:
        out_path = os.path.join(OUTPUT_DIR, f"{unit_id}.txt")
        if os.path.exists(out_path):
            print(f"  Already exists, skipping: {unit_id}")
            continue
        html = load_page_with_unit_card(unit_id)
        if html:
            save_unit(unit_id, html)
        else:
            print(f"  Unit card not found for {unit_id}, skipping.")
            error_ws.append([set_abbr, "Unit not found", unit_id])

driver.quit()
error_wb.save(ERROR_FILE)
print(f"\nSaved errors to {ERROR_FILE}")
print("\nDone.")
