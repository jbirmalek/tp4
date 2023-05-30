<script type="text/javascript">
    $(function () {
        // Document.ready -> link up remove event handler
        $(".PlusProduit").click(function () {
            // Get the id from the link
            var recordtoupdate = $(this).attr("data-id");
            if (recordtoupdate != '') {
                // Perform the ajax post
                $.post("/Panier/PlusProduit", { "id": recordtoupdate },
                    function (data) {
                        if (data.ct == "1") {
                            $('#totalapayer').text(data.Total);
                            $("#quantite_" + recordtoupdate).text(data.Quatite);
                            $("#rquantite_" + recordtoupdate).text(data.Quatite);
                            $("#total_" + recordtoupdate).text(data.TotalRow);
                        }
                    });
            }
        });
    // Document.ready -> link up remove event handler
    $(".MinusProduit").click(function () {
// Get the id from the link
var recordtoupdate = $(this).attr("data-id");
    if (recordtoupdate != '') {
        // Perform the ajax post
        $.post("/Panier/MoinsProduit", { "id": recordtoupdate },
            function (data) {
                if (data.ct == "1") {
                    $('#totalapayer').text(data.Total);
                    $("#quantite_" + recordtoupdate).text(data.Quatite);
                    $("#rquantite_" + recordtoupdate).text(data.Quatite);
                    $("#total_" + recordtoupdate).text(data.TotalRow);
                }
                else if (data.ct == "0") {
                    $("#row-" + recordtoupdate).fadeOut('slow');
                }
            });
}
});
    // Document.ready -> link up remove event handler
    $(".RemoveLink").click(function () {
// Get the id from the link
var recordtoupdate = $(this).attr("data-id");
    if (recordtoupdate != '') {
        // Perform the ajax post
        $.post("/Panier/SupprimerProduit", { "id": recordtoupdate },
            function (data) {
                $("#row-" + recordtoupdate).fadeOut('slow');
                $('#totalapayer').text(data.Total);
            });
}
});
});
</script>
