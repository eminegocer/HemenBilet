document.addEventListener("DOMContentLoaded", function () {
    const tripTypeInputs = document.querySelectorAll('input[name="tripType"]');
    const returnDateContainer = document.getElementById("returnDateContainer");
    const contactLink = document.getElementById("contactLink");
    const contactPopup = document.getElementById("contactPopup");
    const closePopup = document.getElementById("closePopup");

    // Gidiþ-Dönüþ seçeneðine göre dönüþ tarihi alanýný göster/gizle
    tripTypeInputs.forEach(input => {
        input.addEventListener("change", function () {
            if (this.value === "round-trip") {
                returnDateContainer.style.display = "flex";
                document.getElementById("returnDate").setAttribute("required", "required");
            } else {
                returnDateContainer.style.display = "none";
                document.getElementById("returnDate").removeAttribute("required");
            }
        });
    });

    // Ýletiþim linkine týklanýnca pop-up'ý göster
    contactLink.addEventListener("click", function (event) {
        event.preventDefault(); // Linkin varsayýlan davranýþýný engelle
        contactPopup.style.display = "flex"; // Pop-up'ý göster
    });

    // Çarpý iþaretine týklanýnca pop-up'ý kapat
    closePopup.addEventListener("click", function () {
       
        contactPopup.style.display = "none"; // Pop-up'ý gizle
    });

    // Pop-up dýþýna týklanýnca pop-up'ý kapat
    contactPopup.addEventListener("click", function (event) {
        if (event.target === contactPopup) {
            contactPopup.style.display = "none"; // Pop-up'ý gizle
        }
    });

    // Nereden ve Nereye alanlarýný yer deðiþtir
    const swapIcon = document.getElementById("swapIcon");
    swapIcon.addEventListener("click", function () {
        const fromInput = document.getElementById("from");
        const toInput = document.getElementById("to");
        const tempValue = fromInput.value;
        fromInput.value = toInput.value;
        toInput.value = tempValue;
    });
});
