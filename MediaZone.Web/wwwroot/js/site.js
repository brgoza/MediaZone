window.onload = function () {
    initGallery();
    UpdateTree();
};


function FolderChanged() {
    let treeElement = document.getElementById("tree");
    let folderId = treeElement.getSelectedValues()[0];
    document.getElementById("parentIdInput").value = folderId;
    document.getElementById("active-folder-id").value = folderId;   


    UpdateFolderContentsArea(folderId);
    
}

function UpdateTree(callback) {
    $.ajax({
        url: 'api/FolderApi/GetTree',
        method: 'GET',
        dataType: 'JSON',
        success: function (response) {
            document.getElementById("tree").dataSource = response;
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}




function UpdateFolderContentsArea(folderId) {
    $.ajax({
        url: 'Home/GetFolderContents',
        method: 'GET',
        data: { folderId: folderId },
        success: function (response) {
            document.getElementById("folder-contents").innerHTML = response;
            getNewImages(folderId);
            $("#upload-input").fileinput(
                {
                    'uploadUrl': '/api/ImagesApi',
                    'previewFileType': 'any',
                  
                    uploadExtraData: { folderId: folderId }
                });

        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}
function UpdateFolderActionsArea(folderId) {
    $.ajax({
        url: 'Home/GetFolderContents',
        method: 'GET',
        data: { folderId: folderId },
        success: function (response) {
            console.log(response);
            document.getElementById("folder-contents").innerHTML = response;
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}



