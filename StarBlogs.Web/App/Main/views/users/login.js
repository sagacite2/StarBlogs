(function () {

    $('#loginButton').click(function (e) {
        e.preventDefault();
        abp.ui.setBusy(
            $('#loginButton'),
            abp.ajax({
                url: abp.appPath + 'Account/Login',
                type: 'POST',
                data: JSON.stringify({
                    username: $('#UsernameInput').val(),
                    password: $('#PasswordInput').val(),
                    rememberMe: $('#RememberMeInput').is(':checked')
                })
            })
        );
    });

})();