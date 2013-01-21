var SentMessages = {};
SentMessages.dataSource = null;
SentMessages.tripDataSource = null;
SentMessages.init = function () {
    SentMessages.dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url:"api/Messages"
            }
        },
        batch: true,

        pageSize: 10,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number" },
                    MessageId: { type: "string" },
                    StatusCode: { type: "string" },
                   
                    MessageStatus: { type: "sring" },
                    MessageText: { type: "string" },
                   
                    DateSent: { type: "date" }
                }
            }
        }
    }); //end of datasource

    $("#historyGrid").kendoGrid({
        dataSource: SentMessages.dataSource,
        pageable: true,
        sortable: true,
        filterable: true,
        groupable: true,
        height: 500,
        columns: [
//        {field:"Id",editable:false},
        { title: "Message Id", field: "MessageId" },
        { title: "Status Code", field: "StatusCode" },
        { title: "Status", field: "MessageStatus" },
        { title: "Message", field: "MessageText" },
        { title: "Date Sent", field: "DateSent" }
    ],
        editable: false,
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        dataBound: function () {
            this.expandRow(this.tbody.find("tr.k-master-row").first());
        }
    }); //end of kendoGrid

    function detailInit(e) {
        var detailRow = e.detailRow;
        
        detailRow.find(".tabstrip").kendoTabStrip({
            animation: {
                open: { effects: "fadeIn" }
            }
        });

        detailRow.find(".trips").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: 'api/Messages/Numbers/'+e.data.Id
                        
                    }
                    
                }

            },
            scrollable: false,
            sortable: true,
            pageable: true,
            filterable:true,
            columns: [
                            {title:"Phone Numbers", field: "Number",filterable:true }
                        ]
        });
    } // end of detail init
}      //end of init