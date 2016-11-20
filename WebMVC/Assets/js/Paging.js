$(document).ready(function () {
    $('#search').keyup(function () {
        $('#table-statis').DataTable().search($('#search').val()).draw();
        CreateChangePage($('#table-statis').DataTable().page.info().pages, $('#table-statis').DataTable().page.info().page + 1);
    });
    $('#ChangeRow').change(function () {
        console.log("ok");
        $('#table-statis').DataTable().page.len($('#ChangeRow').val()).draw();
        CreateChangePage($('#table-statis').DataTable().page.info().pages, $('#table-statis').DataTable().page.info().page + 1);
    });
    $('#ChangePage').bootpag({
        total: 1,
        page: 1,
        maxVisible: 5,
        next: '>',
        prev: '<',
        firstLastUse: true,
        first: '<<',
        last: '>>'
    }).on('page', function (event, num) {
        $('#table-statis').DataTable().page(num - 1).draw(false);
    });
    function CreateChangePage(TotalPage, Page) {
        $('#ChangePage').bootpag({
            total: TotalPage,
            page: Page
        });
    };
    $('#table-statis').DataTable({
        columnDefs: [{ orderable: false, targets: [0] }],
        "info": false,
        "paging": true
    });
    $('#table-statis').DataTable().page.len(20).draw();
    CreateChangePage($('#table-statis').DataTable().page.info().pages, $('#table-statis').DataTable().page.info().page + 1);
    //function search() {
    //    console.log("ok");
    //    var searchTerm = $(".search").val();
    //    var listItem = $('.results tbody').children('tr');
    //    var searchSplit = searchTerm.replace(/ /g, "'):containsi('")

    //    $.extend($.expr[':'], {
    //        'containsi': function (elem, i, match, array) {
    //            return (elem.textContent || elem.innerText || '').toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
    //        }
    //    });

    //    $(".results tbody tr").not(":containsi('" + searchSplit + "')").each(function (e) {
    //        $(this).attr('visible', 'false');
    //    });

    //    $(".results tbody tr:containsi('" + searchSplit + "')").each(function (e) {
    //        $(this).attr('visible', 'true');
    //    });

    //    var jobCount = $('.results tbody tr[visible="true"]').length;
    //    $('.counter').text(jobCount + ' item');

    //    if (jobCount == '0') { $('.no-result').show(); }
    //    else { $('.no-result').hide(); }
    //    CreateChangePage($('#table-statis').DataTable().page.info().pages, $('#table-statis').DataTable().page.info().page + 1);
    //}
});