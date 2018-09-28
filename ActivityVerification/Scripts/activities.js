$(document).ready(function () {
    toggleReason();
});

$("[name='Type']").change(function () {
    toggleReason();
});

function toggleReason() {
    $("[name='Type']").val() === '2' ? $(".recogDiv").show() : $(function () { $(".recogDiv").hide(); $("[name='RecognitionReason']").val(0) });
}
