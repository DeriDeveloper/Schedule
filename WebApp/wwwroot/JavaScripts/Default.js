function clickElement(element) {
    element.click();
}

function getFileList(element) {
    return new Promise((resolve) => {
        const fileList = [];
        const files = element.files;

        // Create an array of promises for each file
        const filePromises = [];

        for (let i = 0; i < files.length; i++) {
            const file = files.item(i);
            const fileInfo = {
                name: file.name,
                size: file.size,
                data: null // Add a property to store the file data
            };
            fileList.push(fileInfo);

            const reader = new FileReader();

            const promise = new Promise((fileResolve) => {
                reader.onload = function (e) {
                    const fileData = new Uint8Array(e.target.result);

                    // Convert the file data to an array of bytes
                    const byteData = Array.from(fileData);
                    fileInfo.data = byteData; // Store the file data as an array of bytes

                    fileResolve(); // Resolve the individual file promise
                };
            });

            reader.readAsArrayBuffer(file);
            filePromises.push(promise);
        }

        // Wait for all file promises to resolve
        Promise.all(filePromises).then(() => {
            resolve(fileList);
        });
    });
}