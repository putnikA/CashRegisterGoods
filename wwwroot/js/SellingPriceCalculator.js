function calculateSellingPrice() {
    var netPrice = parseFloat($('#NetPrice').val());
    var pdvValue = parseFloat($('#PDV').val()) / 100;
    var marginValue = parseFloat($('#Margin').val()) / 100;
    var sellingPricePerUnit = netPrice * (1 + pdvValue) * (1 + marginValue);
    $('#SellingPricePerUnit').val(sellingPricePerUnit.toFixed(2));
}

$(document).ready(function () {
    $('#NetPrice').on('input', function () {
        calculateSellingPrice();
    });

    $('#PDV').on('input', function () {
        calculateSellingPrice();
    });

    $('#Margin').on('input', function () {
        calculateSellingPrice();
    });

    // Initially calculate the Selling Price Per Unit
    calculateSellingPrice();
});