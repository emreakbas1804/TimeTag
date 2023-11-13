window.onload = function() {
    // "Authorize" düğmesi tıklandığında çalışacak kod
    document.querySelector(".authorize-wrapper button").addEventListener("click", function() {
        // Token input alanını seç
        var tokenInput = document.querySelector(".authorize-wrapper input[name='access_token']");

        // Bearer kelimesini ekleyerek token input alanına yaz
        tokenInput.value = "Bearer " + tokenInput.value;
    });
};
