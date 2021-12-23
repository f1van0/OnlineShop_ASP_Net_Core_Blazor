// const { jsPDF } = window.jspdf;

window.blazorExtensions = {
    SetCookie: function (name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        } else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    GetCookie: function (cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    },
    TakeScreenshot: async function (id) {
        var img = "";
        await html2canvas(document.querySelector("#" + id)).then(canvas => img = canvas.toDataURL("image/png"));
        var d = document.createElement("a");
        d.href = img;
        d.download = "image.png";
        d.click();
        return img;
    },
    Print: async function (fragment, filename = "report") {
        var element = document.getElementById(fragment);
        var opt = {
            margin: 1,
            filename: filename + '.pdf',
            image: {type: 'jpeg', quality: 0.98},
            html2canvas: {scale: 1},
            jsPDF: {unit: 'in', format: 'letter', orientation: 'landscape'}
        };
        html2pdf().set(opt).from(element).save();
        return null;
    }
}