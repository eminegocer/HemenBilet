$(document).ready(function () {
    var selectedSeat = null;

    // Koltuk seçimi işlemi
    $(document).on("change", ".seat-checkbox:not([data-reserved='true'])", function () {
        var $this = $(this);

        // Önceki seçimi kaldır
        $(".seat-checkbox").not(this).prop("checked", false);

        // Yeni seçimi güncelle
        selectedSeat = $this.prop("checked") ? $this : null;

        // Seçili koltuk bilgisini güncelle
        var seatNumber = $this.data("seat");
        var seatId = $this.val(); // Seçilen koltuğun ID'si
        var flightId = $("#flightId").val(); // Flight ID'sini al

        if (selectedSeat) {
            // Koltuk seçildiğinde
            $("#selectedSeatInfo").show();
            $("#seatNumber").text(seatNumber || "Bilinmiyor");
            $("#selectedSeatId").val(seatId);
            $("#reserveButton").prop("disabled", false);
        } else {
            // Seçim kaldırıldığında
            $("#selectedSeatInfo").hide();
            $("#seatNumber").text("");
            $("#selectedSeatId").val("");
            $("#reserveButton").prop("disabled", true);
        }

        // Kontrol için loglar
        console.log("Seçili Koltuk Bilgileri:");
        console.log("SeatId:", seatId || "Seçili değil");
        console.log("FlightId:", flightId || "Belirtilmemiş");
    });

    // Sayfa yüklendiğinde tüm checkbox'ları sıfırla
    $(".seat-checkbox").prop("checked", false);

    // Sayfa yüklendiğinde rezerve edilmiş koltukları devre dışı bırak
    $('.seat-checkbox[data-reserved="true"]')
        .prop("checked", false)
        .prop("disabled", true);

    // Form gönderim kontrolü
    $("form").on("submit", function (e) {
        var seatId = $("#selectedSeatId").val();
        var flightId = $("#flightId").val();

        if (!seatId || !flightId) {
            e.preventDefault(); // Form gönderimini durdur
            alert("Lütfen bir koltuk seçtiğinizden ve uçuş bilgisinin dolu olduğundan emin olun.");
            console.error("Form Gönderimi Engellendi. Eksik Bilgiler:");
            console.error("SeatId:", seatId || "Boş");
            console.error("FlightId:", flightId || "Boş");
        } else {
            console.log("Form Gönderiliyor. Bilgiler:");
            console.log("SeatId:", seatId);
            console.log("FlightId:", flightId);
        }
    });
});
