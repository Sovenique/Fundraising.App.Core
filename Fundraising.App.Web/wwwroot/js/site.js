
function GetProjects() {

    $.ajax({
        url: '/api/ProjectsAPI',
        method: 'GET',
        contentType: 'application/json'
    })
        // DONE 
        // -------------------------------
        .done(response => {
            console.log(response);
            

            var table = new Tabulator("#tabulator_id", {
                data: response,
                //autoColumns: true,
                layout: "fitColumns",
                sortOrderReverse: true,
                selectable: true,
                columns: [
                    { title: "title", field: "title", headerFilter: "input"},
                    { title: "description", field: "description" },
                    { title: "amountGathered", field: "amountGathered" },
                    { title: "targetAmount", field: "targetAmount" }
                ]
            })

        })
        // FAIL
        // -------------------------------
        .fail(failure => {
            $('#main').html('ERROR');


        })
        // ALWAYS
        // -------------------------------
        .always(response => {
            console.log(response);

        });

} 


$(document).ready(GetProjects());