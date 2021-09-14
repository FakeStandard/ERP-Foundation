Vue.config.devtools = true;
Vue.config.debug = true;

var userVM = new Vue({
    el: '#userApp',
    data: {
        loading: true,
        list: [],
        statusItem: [],
        positionItem: [],
        departmentItem: [],
        unitItem: [],
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
            account: '',
            department: '',
            unit: '',
            position: '',
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
            account: '',
            department: '',
            unit: '',
            position: '',
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
        POSITION: true,
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
        getDepartmentParentItem: function () {
            $.ajax({
                url: GetDepartmentParentItemURL,
                type: 'GET',
                success: function (res) {
                    userVM.$data.departmentItem = res;
                },
                error: function (res) {
                    console.log(err);
                }
            });
        },
        getPositionItem: function () {
            $.ajax({
                url: GetPositionItemURL,
                type: 'GET',
                success: function (res) {
                    userVM.$data.positionItem = res;
                },
                error: function (err) {
                    console.log(err);
                }
            });
        },
        getStatusItem: function () {
            $.ajax({
                url: GetStatusItemURL,
                type: 'GET',
                success: function (res) {
                    userVM.$data.statusItem = res;
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
                englishName: '',
                account: '',
                department: '',
                unit: '',
                position: '',
                tel: '',
                address: '',
                contactPerson: '',
                contactTel: '',
                status: ''
            }

            userVM.$data.addForm.department = this.departmentItem[0].key;
            userVM.$data.addForm.unit = this.departmentItem[0].units[0].key;
            userVM.$data.unitItem = this.departmentItem[0].units;
            userVM.$data.addForm.position = this.positionItem[0].id;
            userVM.$data.addForm.status = this.statusItem[0].id;

            $('#AddUserModal').modal('show');
        },
        showEditDialogClick(item) {
            var departmenetID;
            var unitID;
            for (var i = 0; i < this.departmentItem.length; i++) {
                if (this.departmentItem[i].value === item.department) {
                    departmenetID = this.departmentItem[i].key;
                    userVM.$data.unitItem = this.departmentItem[i].units;

                    for (var j = 0; j < this.departmentItem[i].units.length; j++) {
                        if (this.departmentItem[i].units[j].value === item.unit) {
                            unitID = this.departmentItem[i].units[j].key;
                            break;
                        }
                    }
                }
            }

            var positionID;
            for (var j = 0; j < this.positionItem.length; j++) {
                if (this.positionItem[j].name === item.position) {
                    positionID = this.positionItem[j].id;
                }
            }
            var statusID;
            for (k = 0; k < this.statusItem.length; k++) {
                if (this.statusItem[k].name === item.status) {
                    statusID = this.statusItem[k].id;
                }
            }

            this.$data.editForm = {
                id: item.id,
                name: item.name,
                englishName: item.englishName,
                account: item.account,
                department: departmenetID,
                unit: unitID,
                position: positionID,
                tel: item.tel,
                address: item.address,
                contactPerson: item.contactPerson,
                contactTel: item.contactTel,
                status: statusID
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
        },
        departmentAddChange: function (event) {
            for (var i = 0; i < this.departmentItem.length; i++) {
                if (this.departmentItem[i].key === parseInt(event.target.value)) {
                    this.unitItem = this.departmentItem[i].units;
                    userVM.$data.addForm.unit = this.departmentItem[0].units[0].key;
                    break;
                }
            }
        },
        departmentEditChange: function (event) {
            for (var i = 0; i < this.departmentItem.length; i++) {
                if (this.departmentItem[i].key === parseInt(event.target.value)) {
                    this.unitItem = this.departmentItem[i].units;
                    titleVM.$data.editForm.unitID = this.departmentItem[0].units[0].key;
                    break;
                }
            }
        }
    },
    mounted: function () {
        this.getData();
        this.getDepartmentParentItem();
        this.getPositionItem();
        this.getStatusItem();
    }
});