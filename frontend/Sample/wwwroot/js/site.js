showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        },
        error: function (res) {
            console.log(res);
        }
    })
}

sendForm = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                var parser = new DOMParser();
                var htmlDoc = parser.parseFromString(res, 'text/html');
                var hiddenInput = htmlDoc.querySelector('#SucessResult');
                if (hiddenInput.checked) {
                    $('#view-all').html('')
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    window.location.reload();
                } else {
                    $('#form-modal .modal-body').html(res);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

