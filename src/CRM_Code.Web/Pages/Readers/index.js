$(function () {
    var l = abp.localization.getResource("CRM_Code");
	
	var readerService = window.cRM_Code.readers.readers;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Readers/CreateModal",
        scriptUrl: "/Pages/Readers/createModal.js",
        modalClass: "readerCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Readers/EditModal",
        scriptUrl: "/Pages/Readers/editModal.js",
        modalClass: "readerEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            nameSurname: $("#NameSurnameFilter").val(),
			emailAddress: $("#EmailAddressFilter").val(),
			gender: $("#GenderFilter").val(),
			bookId: $("#BookFilter").val()
        };
    };

    var dataTable = $("#ReadersTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(readerService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('CRM_Code.Readers.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.reader.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('CRM_Code.Readers.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    readerService.delete(data.record.reader.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "reader.nameSurname" },
			{ data: "reader.emailAddress" },
            {
                data: "reader.gender",
                render: function (gender) {
                    if (gender === undefined ||
                        gender === null) {
                        return "";
                    }

                    var localizationKey = "Enum:Gender." + gender;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
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

    $("#NewReaderButton").click(function (e) {
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
    
                $('#BookFilter').select2({
                ajax: {
                    url: abp.appPath + 'api/app/readers/book-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 }
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });
                        mappedItems.unshift({ id: "", text: ' - ' });

                        return { results: mappedItems };
                    }
                }
            });
        
});
