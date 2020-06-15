

$("#processUrl").click(function () {

    $.ajax({
        type: "POST",
        url: "https://localhost:44343/api/words",
        data: JSON.stringify({ url: $("#inputUrl").val() }),
        success: function (data) { 
            $("#wordCloud").empty();
          var values =  data.map((word) => ({ text: word.word, weight: word.count }));
            $('#wordCloud').jQCloud(values);
        },
        'contentType': 'application/json'
    }); 
}); 



