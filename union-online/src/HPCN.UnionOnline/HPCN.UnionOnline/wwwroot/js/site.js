// Write your Javascript code.

function subtract(sender) {
    var quantity = parseInt($(sender).next().val());
    if (isNaN(quantity)) quantity = 1;
    quantity--;
    if (quantity <= 0) quantity = 1;

    $(sender).next().val(quantity);
}

function add(sender) {
    var quantity = parseInt($(sender).prev().val());
    if (isNaN(quantity)) quantity = 1;
    quantity++;

    $(sender).prev().val(quantity);
}

function updateQuantity(sender, doUpdate, cartProductId) {
    disableQuantityUpdate(sender);

    doUpdate(sender);

    var quantity = $(sender).siblings("input[name='Quantity']").val();
    $.post("/cart/update", { CartProductId: cartProductId, Quantity: quantity })
        .done(function (data) {
            enableQuantityUpdate(sender);
        });
}

function disableQuantityUpdate(sender) {
    $(sender).attr("disabled", "disabled");
    $(sender).siblings().attr("disabled", "disabled");
}

function enableQuantityUpdate(sender) {
    $(sender).removeAttr("disabled");
    $(sender).siblings().removeAttr("disabled");
}