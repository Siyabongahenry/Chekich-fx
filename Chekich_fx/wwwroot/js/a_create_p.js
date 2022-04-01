function FileInputChange(input) {
    var img = document.getElementById('ProductImage');
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            img.src = e.target.result;
            img.display = "initial";
        }

        reader.readAsDataURL(input.files[0]);
    }
}