Vue.config.devtools = true;
Vue.config.debug = true;

var userVM = new Vue({
    el: '#userApp',
    data: {
        loading: true,
        list: [],
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
            englishName: '',
            department: '',
            title: '',
            tel: '',
            address: '',
            contactPerson: '',
            contactTel: '',
            status: ''
        },
        editForm: {
            id: '',
            name: '',
            englishName: '',
            department: '',
            title: '',
            tel: '',
            address: '',
            contactPerson: '',
            contactTel: '',
            status: ''
        },
        // 欄位顯示開關
        NAME: true,
        ENGLISHNAME: true,
        DEPARTMENT: true,
        TITLE: true,
        TEL: true,
        STATUS: true,
        // 欄位排序
        //sortNAME: null,
        //sortCONTACTPERSON: null,
        //sortADDRESS: null
    },
    methods: {
        getData: function () {
            this.loading = true;

            $.ajax({
                url: GetUserURL,
                data: {
                    Page: this.page,
                    PageSize: this.pageSize,
                    SearchText: this.searchText
                },
                type: 'GET',
                success: function (res) {
                    userVM.$data.list = res.data;
                },
                error: function (err) {
                    console.log(err);
                },
                complete: function () {
                    userVM.$data.loading = false;
                }
            });
        },
        searchClick() {
            this.getData();
        },
        showAddDialogClick() {
            this.$data.addForm = {
                name: '',
                englishName: '',
                department: '',
                title: '',
                tel: '',
                address: '',
                contactPerson: '',
                contactTel: '',
                status: ''
            }

            $('#AddUserModal').modal('show');
        },
        showEditDialogClick(item) {
            this.$data.editForm = {
                id: item.id,
                name: item.name,
                englishName: item.englishName,
                department: item.department,
                title: item.title,
                tel: item.tel,
                address: item.address,
                contactPerson: item.contactPerson,
                contactTel: item.contactTel,
                status: item.status
            }

            $('#EditUserModal').modal('show');
        },
        addData() {
            $('#AddUserModal').modal('hide');

            $.ajax({
                url: AddUserURL,
                data: this.$data.addForm,
                type: 'POST',
                success: function (res) {
                    swal({
                        title: '新增成功！',
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    userVM.getData();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        editData() {
            $('#EditUserModal').modal('hide');

            $.ajax({
                url: EditUserURL,
                data: this.$data.editForm,
                type: 'POST',
                success: function () {
                    swal({
                        title: "儲存成功！",
                        type: 'success',
                        showConfirmButton: false,
                        timer: 1000
                    }).catch(swal.noop)

                    userVM.getData();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    },
    mounted: function () {
        this.getData();
    }
});