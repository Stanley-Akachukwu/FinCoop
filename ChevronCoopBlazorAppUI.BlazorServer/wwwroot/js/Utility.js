
    function timeOutCall(dotnethelper){
        document.onmousemove =resetTimeDelay;
        document.onkeypress=resetTimeDelay;
        function resetTimeDelay(){
            //dotnethelper.invokeMethodeAsync("TimerInterval")
            clearTimeout(timer);
            timer = setTimeout(logout, 3000);
            function logout() {
                dotnethelper.InvokeMethodAsync("Logout");
            }
        }

}
function initializeInactivityTimer(dotnetHelper) {
    var timer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, 600000000);
    }

    function logout() {
        dotnetHelper.invokeMethodAsync("Logout");
    }

}
window.saveAsFile = (filename, bytesBase64) => {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
} 

window.downloadFile = (fileName, fileBytes) => {
    const blob = new Blob([fileBytes], { type: 'application/octet-stream' });
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName;
    document.body.appendChild(anchorElement);
    anchorElement.click();
    document.body.removeChild(anchorElement);
    URL.revokeObjectURL(url);
};

function BlazorDownloadFile(filename, contentType, data) {

    // Create the URL
    const file = new File([data], filename, { type: contentType });
    const exportUrl = URL.createObjectURL(file);

    // Create the <a> element and click on it
    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = filename;
    a.target = "_self";
    a.click();

    // We don't need to keep the url, let's release the memory
    // On Safari it seems you need to comment this line... (please let me know if you know why)
    URL.revokeObjectURL(exportUrl);
    a.remove();
}

