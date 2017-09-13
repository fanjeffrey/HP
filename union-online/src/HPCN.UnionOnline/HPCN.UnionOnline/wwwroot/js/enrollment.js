
// remove a TR
function removeRow(sender) {
    var tr = $(sender).closest("tr");
    tr.css("background-color", "#555555");
    tr.fadeOut(200, function () {
        tr.remove();
        // re-number
        renumber($("#choices-table"));
    });
}

function appendRow(sender) {
    var tbody = $(sender).closest("table").find("tbody");
    var countOfRows = tbody.find("tr").length;
    tbody.append('<tr>'
        + '<td>' + (countOfRows + 1) + '</td>'
        + '<td><input name="ValueChoices[' + countOfRows + '].Value" /></td>'
        + '<td><input name="ValueChoices[' + countOfRows + '].DisplayText" class="choice-display-text" /></td>'
        + '<td><input name="ValueChoices[' + countOfRows + '].DisplayOrder" class="choice-display-order" /></td>'
        + '<td><input name="ValueChoices[' + countOfRows + '].Description" class="choice-description" /></td>'
        + '<td>' + (countOfRows >= 2 ? '<button onclick="removeRow(this);return false;">Remove</button>' : '') + '</td>'
        + '</tr>');
}

function renumber(table) {
    var tbody = table.find('tbody');
    tbody.find('tr').each(function (trIndex, tr) {
        $(tr).children().first().text(trIndex + 1);
        $(tr).find('td input').each(function (i, textbox) {
            var newName = $(textbox).attr('name').replace(/\[\d+\]/, "[" + trIndex + "]");
            $(textbox).attr('name', newName);
        });
    });
}
