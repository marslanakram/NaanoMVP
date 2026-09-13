// Naano MVP - lightweight UI interactions (vanilla JS only)
document.addEventListener('DOMContentLoaded', function () {
    // Auto-dismiss alerts after a few seconds
    document.querySelectorAll('.alert').forEach(function (alertEl) {
        setTimeout(function () {
            var bsAlert = bootstrap.Alert.getOrCreateInstance(alertEl);
            if (bsAlert) {
                bsAlert.close();
            }
        }, 5000);
    });

    // Debounced live search on the Creators marketplace page (progressive enhancement;
    // the form also submits normally via the "Apply Filters" button).
    var searchInput = document.querySelector('input[name="search"]');
    if (searchInput) {
        var timer;
        searchInput.addEventListener('input', function () {
            clearTimeout(timer);
            timer = setTimeout(function () {
                searchInput.form.submit();
            }, 600);
        });
    }
});
