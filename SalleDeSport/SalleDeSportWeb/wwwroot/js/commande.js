var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/TableauDeBord/GetAll"
        },
        "columns": [
            { "data": "id", "widht": "5%" },
            { "data": "nom", "widht": "25%" },
            { "data": "prenom", "widht": "15%" },
            //{ "data": "abonnement.nom", "widht": "15%" },
            { "data": "dateDebutAbonnement", "widht": "15%" },
            { "data": "dateFintAbonnement", "widht": "10%" },
            { "data": "statutDeCommande", "widht": "10%" },

        ]
    });
}

