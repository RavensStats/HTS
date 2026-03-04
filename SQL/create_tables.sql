CREATE DATABASE IF NOT EXISTS Heroclix;
USE Heroclix;

-- ============================================================
-- AppVersion Table
-- ============================================================
CREATE TABLE IF NOT EXISTS AppVersion (
    id              INT          NOT NULL AUTO_INCREMENT,
    VersionNumber   VARCHAR(20)  NOT NULL,
    Released_At     DATETIME     DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
);

-- ============================================================
-- Units Table
-- ============================================================
CREATE TABLE IF NOT EXISTS Units (
    Set_Abbreviation        VARCHAR(20)  NOT NULL,
    Set_Name                VARCHAR(100),
    Set_Number              VARCHAR(20)  NOT NULL,
    Unit_Name               VARCHAR(255),
    Base_Size               VARCHAR(20)  DEFAULT '1x1',

    -- Starting Lines (up to 10)
    Starting_Line_1         VARCHAR(10),
    Starting_Line_2         VARCHAR(10),
    Starting_Line_3         VARCHAR(10),
    Starting_Line_4         VARCHAR(10),
    Starting_Line_5         VARCHAR(10),
    Starting_Line_6         VARCHAR(10),
    Starting_Line_7         VARCHAR(10),
    Starting_Line_8         VARCHAR(10),
    Starting_Line_9         VARCHAR(10),
    Starting_Line_10        VARCHAR(10),

    -- Point Values (up to 10)
    Point_Value_1           INT,
    Point_Value_2           INT,
    Point_Value_3           INT,
    Point_Value_4           INT,
    Point_Value_5           INT,
    Point_Value_6           INT,
    Point_Value_7           INT,
    Point_Value_8           INT,
    Point_Value_9           INT,
    Point_Value_10          INT,

    -- Traits (up to 10)
    Trait_1                 VARCHAR(255),
    Trait_1_Ability         TEXT,
    Trait_2                 VARCHAR(255),
    Trait_2_Ability         TEXT,
    Trait_3                 VARCHAR(255),
    Trait_3_Ability         TEXT,
    Trait_4                 VARCHAR(255),
    Trait_4_Ability         TEXT,
    Trait_5                 VARCHAR(255),
    Trait_5_Ability         TEXT,
    Trait_6                 VARCHAR(255),
    Trait_6_Ability         TEXT,
    Trait_7                 VARCHAR(255),
    Trait_7_Ability         TEXT,
    Trait_8                 VARCHAR(255),
    Trait_8_Ability         TEXT,
    Trait_9                 VARCHAR(255),
    Trait_9_Ability         TEXT,
    Trait_10                VARCHAR(255),
    Trait_10_Ability        TEXT,

    -- Special Powers (up to 10)
    Special_Power_1         VARCHAR(255),
    Special_Power_Ability_1 TEXT,
    Special_Power_Type_1    VARCHAR(50),
    Special_Power_2         VARCHAR(255),
    Special_Power_Ability_2 TEXT,
    Special_Power_Type_2    VARCHAR(50),
    Special_Power_3         VARCHAR(255),
    Special_Power_Ability_3 TEXT,
    Special_Power_Type_3    VARCHAR(50),
    Special_Power_4         VARCHAR(255),
    Special_Power_Ability_4 TEXT,
    Special_Power_Type_4    VARCHAR(50),
    Special_Power_5         VARCHAR(255),
    Special_Power_Ability_5 TEXT,
    Special_Power_Type_5    VARCHAR(50),
    Special_Power_6         VARCHAR(255),
    Special_Power_Ability_6 TEXT,
    Special_Power_Type_6    VARCHAR(50),
    Special_Power_7         VARCHAR(255),
    Special_Power_Ability_7 TEXT,
    Special_Power_Type_7    VARCHAR(50),
    Special_Power_8         VARCHAR(255),
    Special_Power_Ability_8 TEXT,
    Special_Power_Type_8    VARCHAR(50),
    Special_Power_9         VARCHAR(255),
    Special_Power_Ability_9 TEXT,
    Special_Power_Type_9    VARCHAR(50),
    Special_Power_10        VARCHAR(255),
    Special_Power_Ability_10 TEXT,
    Special_Power_Type_10   VARCHAR(50),

    -- General Info
    Keywords                TEXT,
    Real_Name               VARCHAR(255),
    Range_Value             INT,
    Movement_Type           VARCHAR(50),
    Attack_Type             VARCHAR(50),
    Defense_Type            VARCHAR(50),
    Damage_Type             VARCHAR(50),

    -- Improved Movement Flags
    Improved_Movement_Elevated          TINYINT(1) DEFAULT 0,
    Improved_Movement_Hindering         TINYINT(1) DEFAULT 0,
    Improved_Movement_Blocking          TINYINT(1) DEFAULT 0,
    Improved_Movement_Destroy_Blocking  TINYINT(1) DEFAULT 0,
    Improved_Movement_Characters        TINYINT(1) DEFAULT 0,
    Improved_Movement_Move_Through      TINYINT(1) DEFAULT 0,
    Improved_Movement_Water             TINYINT(1) DEFAULT 0,
    Improved_Movement_Indoor            TINYINT(1) DEFAULT 0,
    Improved_Movement_Outdoor           TINYINT(1) DEFAULT 0,

    -- Improved Targeting Flags
    Improved_Targeting_Elevated         TINYINT(1) DEFAULT 0,
    Improved_Targeting_Hindering        TINYINT(1) DEFAULT 0,
    Improved_Targeting_Blocking         TINYINT(1) DEFAULT 0,
    Improved_Targeting_Characters       TINYINT(1) DEFAULT 0,
    Improved_Targeting_Adjacent         TINYINT(1) DEFAULT 0,

    Number_of_Targets       INT,

    PRIMARY KEY (Set_Abbreviation, Set_Number)
) ROW_FORMAT=DYNAMIC;

-- ============================================================
-- Dial Table
-- ============================================================
CREATE TABLE IF NOT EXISTS Dial (
    Set_Abbreviation    VARCHAR(20)  NOT NULL,
    Set_Name            VARCHAR(100),
    Set_Number          VARCHAR(20)  NOT NULL,

    -- Speed (up to 30 clicks)
    Speed_Value_1       INT,    Speed_Power_1    TEXT,
    Speed_Value_2       INT,    Speed_Power_2    TEXT,
    Speed_Value_3       INT,    Speed_Power_3    TEXT,
    Speed_Value_4       INT,    Speed_Power_4    TEXT,
    Speed_Value_5       INT,    Speed_Power_5    TEXT,
    Speed_Value_6       INT,    Speed_Power_6    TEXT,
    Speed_Value_7       INT,    Speed_Power_7    TEXT,
    Speed_Value_8       INT,    Speed_Power_8    TEXT,
    Speed_Value_9       INT,    Speed_Power_9    TEXT,
    Speed_Value_10      INT,    Speed_Power_10   TEXT,
    Speed_Value_11      INT,    Speed_Power_11   TEXT,
    Speed_Value_12      INT,    Speed_Power_12   TEXT,
    Speed_Value_13      INT,    Speed_Power_13   TEXT,
    Speed_Value_14      INT,    Speed_Power_14   TEXT,
    Speed_Value_15      INT,    Speed_Power_15   TEXT,
    Speed_Value_16      INT,    Speed_Power_16   TEXT,
    Speed_Value_17      INT,    Speed_Power_17   TEXT,
    Speed_Value_18      INT,    Speed_Power_18   TEXT,
    Speed_Value_19      INT,    Speed_Power_19   TEXT,
    Speed_Value_20      INT,    Speed_Power_20   TEXT,
    Speed_Value_21      INT,    Speed_Power_21   TEXT,
    Speed_Value_22      INT,    Speed_Power_22   TEXT,
    Speed_Value_23      INT,    Speed_Power_23   TEXT,
    Speed_Value_24      INT,    Speed_Power_24   TEXT,
    Speed_Value_25      INT,    Speed_Power_25   TEXT,
    Speed_Value_26      INT,    Speed_Power_26   TEXT,
    Speed_Value_27      INT,    Speed_Power_27   TEXT,
    Speed_Value_28      INT,    Speed_Power_28   TEXT,
    Speed_Value_29      INT,    Speed_Power_29   TEXT,
    Speed_Value_30      INT,    Speed_Power_30   TEXT,

    -- Attack (up to 30 clicks)
    Attack_Value_1      INT,    Attack_Power_1   TEXT,
    Attack_Value_2      INT,    Attack_Power_2   TEXT,
    Attack_Value_3      INT,    Attack_Power_3   TEXT,
    Attack_Value_4      INT,    Attack_Power_4   TEXT,
    Attack_Value_5      INT,    Attack_Power_5   TEXT,
    Attack_Value_6      INT,    Attack_Power_6   TEXT,
    Attack_Value_7      INT,    Attack_Power_7   TEXT,
    Attack_Value_8      INT,    Attack_Power_8   TEXT,
    Attack_Value_9      INT,    Attack_Power_9   TEXT,
    Attack_Value_10     INT,    Attack_Power_10  TEXT,
    Attack_Value_11     INT,    Attack_Power_11  TEXT,
    Attack_Value_12     INT,    Attack_Power_12  TEXT,
    Attack_Value_13     INT,    Attack_Power_13  TEXT,
    Attack_Value_14     INT,    Attack_Power_14  TEXT,
    Attack_Value_15     INT,    Attack_Power_15  TEXT,
    Attack_Value_16     INT,    Attack_Power_16  TEXT,
    Attack_Value_17     INT,    Attack_Power_17  TEXT,
    Attack_Value_18     INT,    Attack_Power_18  TEXT,
    Attack_Value_19     INT,    Attack_Power_19  TEXT,
    Attack_Value_20     INT,    Attack_Power_20  TEXT,
    Attack_Value_21     INT,    Attack_Power_21  TEXT,
    Attack_Value_22     INT,    Attack_Power_22  TEXT,
    Attack_Value_23     INT,    Attack_Power_23  TEXT,
    Attack_Value_24     INT,    Attack_Power_24  TEXT,
    Attack_Value_25     INT,    Attack_Power_25  TEXT,
    Attack_Value_26     INT,    Attack_Power_26  TEXT,
    Attack_Value_27     INT,    Attack_Power_27  TEXT,
    Attack_Value_28     INT,    Attack_Power_28  TEXT,
    Attack_Value_29     INT,    Attack_Power_29  TEXT,
    Attack_Value_30     INT,    Attack_Power_30  TEXT,

    -- Defense (up to 30 clicks)
    Defense_Value_1     INT,    Defense_Power_1  TEXT,
    Defense_Value_2     INT,    Defense_Power_2  TEXT,
    Defense_Value_3     INT,    Defense_Power_3  TEXT,
    Defense_Value_4     INT,    Defense_Power_4  TEXT,
    Defense_Value_5     INT,    Defense_Power_5  TEXT,
    Defense_Value_6     INT,    Defense_Power_6  TEXT,
    Defense_Value_7     INT,    Defense_Power_7  TEXT,
    Defense_Value_8     INT,    Defense_Power_8  TEXT,
    Defense_Value_9     INT,    Defense_Power_9  TEXT,
    Defense_Value_10    INT,    Defense_Power_10 TEXT,
    Defense_Value_11    INT,    Defense_Power_11 TEXT,
    Defense_Value_12    INT,    Defense_Power_12 TEXT,
    Defense_Value_13    INT,    Defense_Power_13 TEXT,
    Defense_Value_14    INT,    Defense_Power_14 TEXT,
    Defense_Value_15    INT,    Defense_Power_15 TEXT,
    Defense_Value_16    INT,    Defense_Power_16 TEXT,
    Defense_Value_17    INT,    Defense_Power_17 TEXT,
    Defense_Value_18    INT,    Defense_Power_18 TEXT,
    Defense_Value_19    INT,    Defense_Power_19 TEXT,
    Defense_Value_20    INT,    Defense_Power_20 TEXT,
    Defense_Value_21    INT,    Defense_Power_21 TEXT,
    Defense_Value_22    INT,    Defense_Power_22 TEXT,
    Defense_Value_23    INT,    Defense_Power_23 TEXT,
    Defense_Value_24    INT,    Defense_Power_24 TEXT,
    Defense_Value_25    INT,    Defense_Power_25 TEXT,
    Defense_Value_26    INT,    Defense_Power_26 TEXT,
    Defense_Value_27    INT,    Defense_Power_27 TEXT,
    Defense_Value_28    INT,    Defense_Power_28 TEXT,
    Defense_Value_29    INT,    Defense_Power_29 TEXT,
    Defense_Value_30    INT,    Defense_Power_30 TEXT,

    -- Damage (up to 30 clicks)
    Damage_Value_1      INT,    Damage_Power_1   TEXT,
    Damage_Value_2      INT,    Damage_Power_2   TEXT,
    Damage_Value_3      INT,    Damage_Power_3   TEXT,
    Damage_Value_4      INT,    Damage_Power_4   TEXT,
    Damage_Value_5      INT,    Damage_Power_5   TEXT,
    Damage_Value_6      INT,    Damage_Power_6   TEXT,
    Damage_Value_7      INT,    Damage_Power_7   TEXT,
    Damage_Value_8      INT,    Damage_Power_8   TEXT,
    Damage_Value_9      INT,    Damage_Power_9   TEXT,
    Damage_Value_10     INT,    Damage_Power_10  TEXT,
    Damage_Value_11     INT,    Damage_Power_11  TEXT,
    Damage_Value_12     INT,    Damage_Power_12  TEXT,
    Damage_Value_13     INT,    Damage_Power_13  TEXT,
    Damage_Value_14     INT,    Damage_Power_14  TEXT,
    Damage_Value_15     INT,    Damage_Power_15  TEXT,
    Damage_Value_16     INT,    Damage_Power_16  TEXT,
    Damage_Value_17     INT,    Damage_Power_17  TEXT,
    Damage_Value_18     INT,    Damage_Power_18  TEXT,
    Damage_Value_19     INT,    Damage_Power_19  TEXT,
    Damage_Value_20     INT,    Damage_Power_20  TEXT,
    Damage_Value_21     INT,    Damage_Power_21  TEXT,
    Damage_Value_22     INT,    Damage_Power_22  TEXT,
    Damage_Value_23     INT,    Damage_Power_23  TEXT,
    Damage_Value_24     INT,    Damage_Power_24  TEXT,
    Damage_Value_25     INT,    Damage_Power_25  TEXT,
    Damage_Value_26     INT,    Damage_Power_26  TEXT,
    Damage_Value_27     INT,    Damage_Power_27  TEXT,
    Damage_Value_28     INT,    Damage_Power_28  TEXT,
    Damage_Value_29     INT,    Damage_Power_29  TEXT,
    Damage_Value_30     INT,    Damage_Power_30  TEXT,

    PRIMARY KEY (Set_Abbreviation, Set_Number),
    FOREIGN KEY (Set_Abbreviation, Set_Number)
        REFERENCES Units (Set_Abbreviation, Set_Number)
        ON DELETE CASCADE
        ON UPDATE CASCADE
) ROW_FORMAT=DYNAMIC;

-- ============================================================
-- Users Table
-- ============================================================
CREATE TABLE IF NOT EXISTS Users (
    id              INT             NOT NULL AUTO_INCREMENT,
    username        VARCHAR(100)    NOT NULL UNIQUE,
    password        VARCHAR(255)    NOT NULL,
    email           VARCHAR(255),
    PRIMARY KEY (id)
);

-- If the Users table already exists without the email column, run this:
-- ALTER TABLE Users ADD COLUMN email VARCHAR(255);

-- ============================================================
-- NewGames Table
-- ============================================================
CREATE TABLE IF NOT EXISTS NewGames (
    id                  INT             NOT NULL AUTO_INCREMENT,
    number_of_players   INT             NOT NULL,
    players             TEXT,
    current_players     INT             DEFAULT 0,
    status              VARCHAR(20)     NOT NULL DEFAULT 'Open',
    points_per_team     INT,
    map_size            VARCHAR(50),
    map_name            VARCHAR(100),
    PRIMARY KEY (id)
);

-- ============================================================
-- GamePlayers Table
-- ============================================================
CREATE TABLE IF NOT EXISTS GamePlayers (
    id              INT             NOT NULL AUTO_INCREMENT,
    game_id         INT             NOT NULL,
    player_name     VARCHAR(100)    NOT NULL,
    status          VARCHAR(20)     NOT NULL DEFAULT 'Active',
    PRIMARY KEY (id),
    FOREIGN KEY (game_id) REFERENCES NewGames (id) ON DELETE CASCADE
);

-- ============================================================
-- TurnOrder Table
-- ============================================================
CREATE TABLE IF NOT EXISTS TurnOrder (
    game_id         INT             NOT NULL,
    turn_order      TEXT,
    active_player   VARCHAR(100),
    PRIMARY KEY (game_id),
    FOREIGN KEY (game_id) REFERENCES NewGames (id) ON DELETE CASCADE
);

-- ============================================================
-- SavedGames Table
-- ============================================================
CREATE TABLE IF NOT EXISTS SavedGames (
    game_id         INT             NOT NULL,
    saved_game      MEDIUMTEXT,
    saved_chat      MEDIUMTEXT,
    PRIMARY KEY (game_id),
    FOREIGN KEY (game_id) REFERENCES NewGames (id) ON DELETE CASCADE
);

-- ============================================================
-- Teams Table
-- ============================================================
CREATE TABLE IF NOT EXISTS Teams (
    team_id         INT             NOT NULL AUTO_INCREMENT,
    team_name       VARCHAR(255)    NOT NULL UNIQUE,
    team_data       MEDIUMTEXT,
    team_points     INT,
    PRIMARY KEY (team_id)
);

-- ============================================================
-- GameActivity Table
-- ============================================================
CREATE TABLE IF NOT EXISTS GameActivity (
    id              INT             NOT NULL AUTO_INCREMENT,
    user_id         INT             NOT NULL,
    game_id         INT             NOT NULL,
    activity        TEXT,
    logged_at       DATETIME        DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    FOREIGN KEY (user_id) REFERENCES Users (id) ON DELETE CASCADE,
    FOREIGN KEY (game_id) REFERENCES NewGames (id) ON DELETE CASCADE
);
