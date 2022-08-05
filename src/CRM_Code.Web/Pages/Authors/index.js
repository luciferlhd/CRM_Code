$(function () {
    var l = abp.localization.getResource("CRM_Code");
	
	var authorService = window.cRM_Code.authors.authors;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Authors/CreateModal",
        scriptUrl: "/Pages/Authors/createModal.js",
        modalClass: "authorCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Authors/EditModal",
        scriptUrl: "/Pages/Authors/editModal.js",
        modalClass: "authorEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			birthdateMin: $("#BirthdateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			birthdateMax: $("#BirthdateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
            active: (function () {
                var value = $("#ActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };

    var dataTable = $("#AuthorsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(authorService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('CRM_Code.Authors.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('CRM_Code.Authors.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    authorService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "name" },
            {
                data: "birthdate",
                render: function (birthdate) {
                    if (!birthdate) {
                        return "";
                    }
                    
					var date = Date.parse(birthdate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            {
                data: "active",
                render: function (active) {
                    return active ? l("Yes") : l("No");
                }
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewAuthorButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
