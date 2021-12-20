window.blazorExtensions = {
    SetCookie: function (name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
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
    TakeScreenshot: async function(id) {
        var img = "";
        await html2canvas(document.querySelector("#" + id)).then(canvas => img = canvas.toDataURL("image/png"));
        var d = document.createElement("a");
        d.href = img;
        d.download = "image.png";
        d.click();
        return img;
    },
    Print: async function (fragment, filename="report"){
        let images = []

        const input = document.getElementById(fragment);

        const promise = new Promise(
            resolve => html2canvas(input).then(canvas => {
                resolve(canvas)
            })
        )

        await Promise.all([promise]).then(canvas => {
            const image = canvas[0].toDataURL('image/png')
            images.push(image)
        })

        images.length > 0 && download(images);

        async function download(images){
            const pdf = new jsPDF();
            const pages = document.getElementById(fragment).offsetHeight / unitToPx('297mm');
            
            for (let i = 0; i < Math.round(pages); i++) {
                i > 0 && i < Math.round(pages) && pdf.addPage()
                pdf.addImage(images[i], 'PNG', 0, 0)
            }
            pdf.save(filename + '.pdf')
        }
    },
}