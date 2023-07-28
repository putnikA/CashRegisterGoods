function validatePositiveNumberInput(inputId, errorMessage) {
    $('#' + inputId).on('input', function () {
        var input = $(this);
        var value = input.val();

        // Validate if the value is not a positive number
        if (value !== "" && (value <= 0 || isNaN(value))) {
            input[0].setCustomValidity(errorMessage);
        } else {
            input[0].setCustomValidity('');
        }
    });
}
