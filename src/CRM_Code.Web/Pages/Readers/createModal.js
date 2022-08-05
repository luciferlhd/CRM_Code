var abp = abp || {};

abp.modals.readerCreate = function () {
    var initModal = function (publicApi, args) {
        var l = abp.localization.getResource("CRM_Code");
        
        
        
        
        
        publicApi.onOpen(function () {
            $('#BookLookup').select2({
                dropdownParent: $('#ReaderCreateModal'),
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

                        return { results: mappedItems };
                    }
                }
            });
        });

        var getNewBookIndex = function () {
            var idTds = $($(document).find("#BookTableRows")).find('td[name="id"]');

            if (idTds.length === 0){
                return 0;
            }

            return parseInt($(idTds[idTds.length -1]).attr("index")) +1;
        };

        var getBookIds = function () {
            var ids = [];
            var idTds = $("#BookTableRows td[name='id']");

            for(var i = 0; i< idTds.length; i++){
                ids.push(idTds[i].innerHTML.trim())
            }

            return ids;
        };

        $('#AddBookButton').on('click', '', function(){
            var $select = $('#BookLookup');
            var id = $select.val();
            var existingIds = getBookIds();
            if (!id || id === '' || existingIds.indexOf(id) >= 0){
                return;
            }

            $("#BookTable").show();

            var displayName = $select.find('option').filter(':selected')[0].innerHTML;

            var newIndex = getNewBookIndex();

            $("#BookTableRows").append(
                '                                <tr style="text-align: center; vertical-align: middle;" index="'+newIndex+'">\n' +
                '                                    <td style="display: none" name="id" index="'+newIndex+'">'+id+'</td>\n' +
                '                                    <td style="display: none">' +
                '                                        <input value="'+id+'" id="SelectedBookIds['+newIndex+']" name="SelectedBookIds['+newIndex+']"/>\n' +
                '                                    </td>\n' +
                '                                    <td style="text-align: left">'+displayName+'</td>\n' +
                '                                    <td style="text-align: right">\n' +
                '                                        <button class="btn btn-danger btn-sm text-light bookDeleteButton" index="'+newIndex+'"> <i class="fa fa-trash"></i> </button>\n' +
                '                                    </td>\n' +
                '                                </tr>'
            );
        });

        $(document).on('click', '.bookDeleteButton', function (e) {
            e.preventDefault();
            var index = $(this).attr("index");
            $("#BookTableRows").find('tr[index='+index+']').remove();

            setTimeout(
                function()
                {
                    var rows = $(document).find("#BookTableRows").find("tr");

                    if (rows.length === 0){
                        $("#BookTable").hide();
                    }

                    for (var i=0; i<rows.length; i++){
                        $(rows[i]).attr('index', i);
                        $(rows[i]).find('th[scope="Row"]').empty();
                        $(rows[i]).find('th[scope="Row"]').append(i+1);
                        $($(rows[i]).find('td[name="id"]')).attr('index', i);
                        $($(rows[i]).find('input')).attr('id', 'SelectedBookIds['+i+']');
                        $($(rows[i]).find('input')).attr('name', 'SelectedBookIds['+i+']');
                        $($(rows[i]).find('button')).attr('index', i);
                    }
                }, 200);
        });
    };

    return {
        initModal: initModal
    };
};
