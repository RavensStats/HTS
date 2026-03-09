<?php
// AdvancedSearch.php
// Returns rows of: Set_Name\tCollectors_Number\tUnit_Name\tPoints;
[REDACTED CREDENTIALS]
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) { die(""); }

$sql = $_POST['SQL'] ?? '';
if (!$sql) { die(""); }

// Replace standard column aliases to match DB schema
$sql = preg_replace('/\bCollectors_Number\b/', 'Set_Number', $sql);
$sql = preg_replace('/\bPoints\b(?!\s*[=<>])/', 'Point_Value_1', $sql);

$result = $conn->query($sql);
$output = "";
if ($result) {
    while ($row = $result->fetch_assoc()) {
        $set_name = $row['Set_Name'] ?? '';
        $cn       = $row['Set_Number'] ?? ($row['Collectors_Number'] ?? '');
        $name     = $row['Unit_Name'] ?? '';
        $pts      = $row['Point_Value_1'] ?? ($row['Points'] ?? '');
        $output  .= $set_name . "\t" . $cn . "\t" . $name . "\t" . $pts . ";";
    }
}
$conn->close();
echo $output;
?>
