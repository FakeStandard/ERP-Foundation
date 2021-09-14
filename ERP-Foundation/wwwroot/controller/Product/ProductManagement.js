Vue.config.devtools = true;
Vue.config.debug = true;

var productVM = new Vue({
    el: '#productApp',
    data: {
        loading: true,
        list: [],
        categoryItem: [],
        searchText: '',
        // 當前頁數
        page: 1,
        // 每頁顯示的筆數
        pageSize: 10,
        // 總頁數
        pageTotal: 0,
        // 總筆數
        total: 0,
        addForm: {
            name: '',
            category: '',
            price: ''
        },
        editForm: {
            id: '',
            name: '',
            category: '',
            price: '',
            image: ''
        },
        uploadID: '',
        uploadName: '',
        // 欄位顯示開關
        NAME: true,
        CATEGORY: true,
        PRICE: true,
        IMAGE: true,
        // 欄位排序
        sortNAME: null,
        sortCATEGORY: null,
        sortPRICE: null,
        sortIMAGE: null
    },
    methods: {
        getData: function () {
            this.loading = true;

            $.ajax({
                url: GetProductURL,
                data: {
                    Page: this.page,
                    PageSize: this.pageSize,
                    SearchText: this.searchText
                },
                type: 'GET',
                success: function (res) {
                    productVM.$data.list = res.data;
                },
                error: function (err) {
                    console.log(err);
                },
                complete: function () {
                    productVM.$data.loading = false;
                }
            });
        },
        getCategoryData() {
            $.ajax({
                url: GetCategoryItemURL,
                type: 'GET',
                success: function (res) {
                    productVM.$data.categoryItem = res;
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        searchClick() {
            this.getData();
        },
        showAddDialogClick() {
            this.$data.addForm = {
                name: '',
                category: '',
                price: ''
            }

            productVM.$data.addForm.category = this.categoryItem[0].key;
            $('#AddProductModal').modal('show');
        },
        showEditDialogClick(item) {
            var key;
            for (var index = 0; index < this.categoryItem.length; index++) {
                if (this.categoryItem[index].value === item.category) {
                    key = this.categoryItem[index].key;
                    break;
                }
            }

            this.$data.editForm = {
                id: item.id,
                name: item.name,
                category: key,
                price: item.price,
                image: item.image
            }

            $('#EditProductModal').modal('show');
        },
        showUploadFileDialogClick() {
            $('#uploadDataFile').val(null);
            fileErrMsg.innerHTML = '';
            $('#UploadProductModal').modal('show');
        },
        showUploadImageDialogClick(id, name) {
            this.uploadID = id;
            this.uploadName = name;
            $('#uploadImageFile').val(null);
            imageErrMsg.innerHTML = '';
            $('#displayImg').attr('src', '');
            $('#btnUploadImage').attr('disabled', true);
            $('#UploadImageModal').modal('show');
        },
        addData() {
            $('#AddProductModal').modal('hide');

            $.ajax({
                url: AddProductURL,
                data: this.$data.addForm,
                type: 'POST',
                success: function (res) {
                    swal({
                        title: '新增成功！',
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    productVM.getData();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        editData() {
            $('#EditProductModal').modal('hide');

            $.ajax({
                url: EditProductURL,
                data: this.$data.editForm,
                type: 'POST',
                success: function () {
                    swal({
                        title: "儲存成功！",
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    productVM.getData();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        deleteDataClick(id) {
            swal({
                title: '確定要刪除？',
                text: '刪除後將無法恢復！',
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: '忍心刪除！',
                cancelButtonText: '取消'
            }).then((confirm) => {
                if (confirm) {
                    $.ajax({
                        url: DeleteProductURL + '/' + id,
                        type: 'POST',
                        success: function () {
                            swal({
                                title: '刪除成功！',
                                text: '您已經刪除該筆資料',
                                type: 'success',
                                timer: 1000,
                                showConfirmButton: false
                            })
                        },
                        error: function () {
                            swal({
                                title: '刪除失敗！',
                                text: '刪除資料失敗,請重新操作',
                                type: 'error',
                                timer: 1000,
                                showConfirmButton: false
                            })
                        },
                        complete: function () {
                            productVM.getData();
                        }
                    });

                }
            })
        },
        uploadDataChange() {
            fileErrMsg.innerHTML = '';

            var upload = $('#uploadDataFile').val().toLowerCase();
            var regex = new RegExp("(.*?)\.(xlsx|xls)$");

            if (regex.test(upload)) {
                $('#btnUploadData').attr('disabled', false);
            } else {
                fileErrMsg.innerHTML = '請上傳 .xls 或 .xlsx';
                $('#btnUploadData').attr('disabled', true);
            }
        },
        uploadImageChange: function (event) {
            imageErrMsg.innerHTML = '';

            var upload = $('#uploadImageFile').val().toLowerCase();
            var regex = new RegExp("(.*?)\.(jpg|jpeg|png)$");

            if (regex.test(upload)) {
                $('#btnUploadImage').attr('disabled', false);

                var input = event.target; // 取得上傳檔案
                var reader = new FileReader(); // 建立 FileReader 物件

                reader.readAsDataURL(input.files[0]);

                reader.onload = function () {
                    var dataURL = reader.result;
                    $('#displayImg').attr('src', dataURL).show();
                };
            } else {
                imageErrMsg.innerHTML = '請上傳 JPG.JPEG.PNG 格式';
                $('#btnUploadImage').attr('disabled', true);
            }
        },
        uploadData() {
            var formData = new FormData();
            var files = document.getElementById('uploadDataFile');
            formData.append(files.files[0].name, files.files[0]);

            $.ajax({
                url: UploadFileURL,
                data: formData,
                type: 'POST',
                //async: false,
                cache: false,
                contentType: false, // 必須是 false 才會自動添加正確的 Content-Type
                processData: false, // 必須是 false 才能避開 jQuery 對 formData 的默認處理,XMLHttpRequest 會對 formData 進行正確的處理
                success: function () {
                    swal({
                        title: 'Success',
                        text: '上傳成功！',
                        type: 'success',
                        timer: 1000,
                        showConfirmButton: false
                    })
                },
                error: function (err) {
                    swal({
                        title: 'Oops...',
                        text: err.responseText,
                        type: 'error',
                        timer: 1000,
                        showConfirmButton: false
                    })
                },
                complete: function () {
                    $('#btnUploadData').attr('disabled', true);
                    $('#UploadProductModal').modal('hide');
                    productVM.getData();
                }
            });
        },
        uploadImage() {
            var formData = new FormData();
            var images = document.getElementById('uploadImageFile');
            for (i = 0; i < images.files.length; i++) {
                formData.append(images.files[i].name, images.files[i]);
            }

            $.ajax({
                url: UploadImageURL + '/' + this.uploadID,
                data: formData,
                type: 'POST',
                //async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function () {
                    swal({
                        title: 'Success',
                        text: '上傳圖片成功！',
                        type: 'success',
                        timer: 1000,
                        showConfirmButton: false
                    })
                },
                error: function (err) {
                    swal({
                        title: 'Oops...',
                        text: err.responseText,
                        type: 'error',
                        timer: 1000,
                        showConfirmButton: false
                    })
                },
                complete: function () {
                    $('#UploadImageModal').modal('hide');
                    productVM.getData();
                }
            });
        }
    },
    mounted: function () {
        this.getData();
        this.getCategoryData();
    }
});