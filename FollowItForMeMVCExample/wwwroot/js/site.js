
const deleteObject = (url) => {

    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message,
                            {
                                timeOut: 1000,
                                preventDuplicates: true,
                                //positionClass: 'toast-top-center',
                                // Redirect ~ TODO: NOT SURE WHY THIS IS NOT REDIRECTING
                                onHidden: function () {
                                    location.href = '/SearchObject/';
                                }
                            })                             
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
          
        } else {
           return false;

        }
    });
} 



