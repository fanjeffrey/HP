
function validate() {
    if (0 == $("input:checked").length) {
        alert("Please select at least one!");
        return false;
    }

    return true;
}