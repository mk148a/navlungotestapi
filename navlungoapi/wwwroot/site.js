// <snippet_SiteJs>
const uri = "api/Job";
let Jobs = null;
function getCount(data) {
  const el = $("#counter");
    let destinationCountry = "taşıyıcı";
  if (data) {
    if (data > 1) {
        destinationCountry = "Taşıyıcılar";
    }
      el.text(data + " " + destinationCountry);
  } else {
      el.text("No " + destinationCountry);
  }
}

// <snippet_GetData>
$(document).ready(function() {
  getData();
});

function getData() {
  $.ajax({
    type: "GET",
    url: uri,
    cache: false,
    success: function(data) {
        const tBody = $("#Jobs");
     

      $(tBody).empty();

      getCount(data.length);
        //.append($("<td></td>").text(Job.cronjob.schedule)).append($("<td></td>").text(Job.cronjob.mailadress))
        $.each(data, function (key, Job) {  
           // alert(Job.toSource())
        const tr = $("<tr></tr>")
            .append($("<td></td>").text(Job.shipper)).append($("<td></td>").text(Job.destinationCountry)).append($("<td></td>").text(Job.schedule)).append($("<td></td>").text(Job.mailadress)).append($("<td></td>").text(Job.crondesc))
          .append(
            $("<td></td>").append(
              $("<button>Edit</button>").on("click", function() {
                  editItem(Job.shipper);
              })
            )
          )
          .append(
            $("<td></td>").append(
              $("<button>Delete</button>").on("click", function() {
                  deleteItem(Job.shipper);
              })
            )
          );

        tr.appendTo(tBody);
      });

      Jobs = data;
    }
  });
}
// </snippet_GetData>






//function Cronjob(mailadress,schedule) {
//    this.mailadress = mailadress;
//    this.schedule = schedule; 
//};
//function Job(shipper,destinationCountry,Cronjob) {
//    this.shipper = shipper;
//    this.destinationCountry = destinationCountry;
//    this.cronjob = Cronjob;
//};

// <snippet_AddItem>
function addItem() {
    //var cronJob = new Cronjob($("#add-mailadress").val(), $("#add-schedule").val());
    //var job = new Job($("#add-shipper").val(), $("#add-destinationCountry").val(), cronJob);
   
    function Job(shipper, destinationCountry, mailadress,schedule)
    {
        this.shipper = shipper;
        this.destinationCountry = destinationCountry;       
        this.mailadress = mailadress;
        this.schedule = schedule;
    };
    var job = new Job($("#add-shipper").val(), $("#add-destinationCountry").val(),$("#add-mailadress").val(),$("#add-schedule").val());
  //  alert(job.toSource());
   // alert(JSON.stringify(job).toSource());
  $.ajax({
    type: "POST",
    accepts: "application/json",
    url: uri,
    contentType: "application/json",
      data: JSON.stringify(job),
    error: function(jqXHR, textStatus, errorThrown) {
        alert(errorThrown);
    },
    success: function(result) {
      getData();
        $("#add-destinationCountry").val("");
        $("#add-mailadress").val("");
        $("#add-schedule").val("");
        $("#add-shipper").val("");
    }
  });
}
// </snippet_AddItem>

function deleteItem(shipper) {
  // <snippet_AjaxDelete>
  $.ajax({
      url: uri + "/" + shipper,
    type: "DELETE",
    success: function(result) {
      getData();
    }
  });
  // </snippet_AjaxDelete>
}

function editItem(shipper) {
    $.each(Jobs, function (key, Job) {
      if (Job.shipper === shipper) {
          $("#edit-destinationCountry").val(Job.destinationCountry);
          $("#edit-shipper").val(Job.shipper);
          $("#edit-mailadress").val(Job.mailadress);
          $("#edit-schedule").val(Job.schedule);        
    }
  });
  $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function() {  
    //var cronJob = new Cronjob($("#edit-mailadress").val(), $("#edit-schedule").val());
    //var job = new Job($("#edit-shipper").val(), $("#edit-destinationCountry").val(), cronJob);
    function Job(shipper, destinationCountry, mailadress, schedule) {
        this.shipper = shipper;
        this.destinationCountry = destinationCountry;
        this.mailadress = mailadress;
        this.schedule = schedule;
    };
    var job = new Job($("#edit-shipper").val(), $("#edit-destinationCountry").val(), $("#edit-mailadress").val(), $("#add-schedule").val());



  // <snippet_AjaxPut>
  $.ajax({
      url: uri + "/" + $("#edit-shipper").val(),
    type: "PUT",
    accepts: "application/json",
    contentType: "application/json",
      data: JSON.stringify(job),
    success: function(result) {
      getData();
    }
  });
  // </snippet_AjaxPut>

  closeInput();
  return false;
});

function closeInput() {
  $("#spoiler").css({ display: "none" });
}
// </snippet_SiteJs>
