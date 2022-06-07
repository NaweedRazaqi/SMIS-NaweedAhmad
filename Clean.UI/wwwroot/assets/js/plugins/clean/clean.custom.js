


// ==================================== Search Student Marks======================================================//
$(document).ready(function () {
    Initialize();
});
function Initialize() {
    $('.select').select2({
        placeholder: '--',
        allowClear: true
    });
    $('#uxsSearch').on('click', function () {
        var StudentSchoolID = $('#uxsstudentschoolid').val();
        var ClassTypeID = $('#uxsclasstypeid').val();
        var ClassManagementID = $('#uxsclassmanagementid').val();
        var ExamTypeID = $('#uxsexamtypeid').val();

        $("#uxsStudentSubject").find('tr').remove();
        var Option = "";

        $.ajax({
            type: 'POST',
            url: 'StudentsSubject/Search',
            dataType: 'json',
            contentType: 'application/json',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: JSON.stringify({ studentschoolid: StudentSchoolID, classtypeid: ClassTypeID, ClassManangementID: ClassManagementID, examtypeid: ExamTypeID }),
            success: function (data) {
                if (data.count != 0) {
                    data.forEach(element => {
                        Option = Option
                            + "<tr row_id=" + element.id + " >" +
                            "<td> " + element.firstName + "</td >" +
                            "<td> " + element.fatherName + "</td >" +
                            "<td>" + element.schoolName + " </td>" +
                            "<td>" + element.classTypeName + " </td>" +
                            "<td class_id = " + element.classCategoryId + ">" + element.classCategory + " </td>" +
                            "<td style='align-content: revert;display: flex;' id='classSubject" + element.id + "' >" + GetSubject(element) + "</td>" +
                            "<td id='TotalBox'>" + "<input type = 'text'/ style='width:70px; position:relative; top:10px;'>" + "</td>" +
                            "<td id='AverageBox'>" + "<input type = 'text'/ style='width:90px;  position:relative; top:10px;'>" + "</td>" +
                            "<td id='Resultbox'>" + "<input type = 'text'/  style='width:70px;  position:relative; top:10px;t;'>" + "</td>"
                            + "</tr > ";
                    });

                }
                $("#uxsStudentSubject").append(Option);
            },
            error: function (error) {

                clean.widget.error("ریکارد شما پیدا نشد")
            }
        });
    });


}

function GetSubject(element) {

    var Name = "";
    element.classSubject.forEach(x => {
        Name = Name + "<span>" + x.name + " : " + "<input type='text' class='marks_value' onmouseout='CalculateMarks(this)' data_id1=" + x.hsscmId + "  data_id=" + x.id + " value =" + x.marks + " style='width:75px; direction:rtl;' /></span>";
    });
    return Name;

}
function CalculateMarks(element) {
    var Total = 0;
    var InputValue = 0;
    var Average = 0;
    var Row_ID = $(element).parent('span').parent('td').parent('tr').attr("row_id");
    var SelectedRows = $('#uxsStudentSubject').find('tr[row_id =' + Row_ID + ']').find('input');
    var clasSub = $('#uxsStudentSubject').find('tr[row_id =' + Row_ID + ']').find('span').find('input');
    var Counter = 0
    var absent = 0;
    var inputnumber = 0

    SelectedRows.each(function () {
        inputnumber++;   // count the number inputs in row
    });

    debugger;
    clasSub.each(function () {
        InputValue = $(this).prop('value');
        if (InputValue < 40) {
            absent = (absent + 1);

        }
        Total = parseInt(Total) + parseInt(InputValue);
        Counter++
        SelectedRows[inputnumber - 3].value = Total;
    });

    Average = Total / Counter;
    SelectedRows[inputnumber - 2].value = Average;


    if (absent == 3) {
        SelectedRows[inputnumber - 1].value = "مشروط";

    }

    else if (Average < 50) {

        SelectedRows[inputnumber - 1].value = "ناکام";

    }
    else {

        SelectedRows[inputnumber - 1].value = "کامیاب";

    }

    $(".marks_value").change(function () {

        var ExamTypeID = $('#uxsexamtypeid').val();
        if (ExamTypeID == 2) {
            if ($(this).val() > 40) {
                $(this).val(0)
                window.confirm("نمره داده شده نباید از 40 بالا باشد لطف نموده دوباره نمره دهی نمایید و دقت نمایید");
                // $("#noty_layout__topRight").show();
            }
        }
        else if (ExamTypeID == 1) {
            if ($(this).val() > 60) {
                $(this).val(0)
                window.confirm("نمره داده شده نباید از 60 بالا باشد لطف نموده دوباره نمره دهی نمایید و دقت نمایید!")


            }
        }
    });


}
// ==================================================End==================================================================//

// ==============================================Save Student Marks===========================================================================//
$('#uxsSave').on('click', function () {

    var StudentClassID = $('#uxsStudentSubject').find("tr");
    var listclasses = [];
    var StudentExamTypeId = $('#uxsexamtypeid').val();

    StudentClassID.each(function () {
        var list = [];

        var clasSub = $('#classSubject' + $(this).attr("row_id")).find("span").find("input");
        var inputID = [];
        var inputValue = [];
        var SCID = $(this).attr("row_id");
        var inputHSSCMId = [];

        clasSub.each(function () {
            inputID.push($(this).attr("data_id"));
            inputValue.push($(this).prop('value'));
            inputHSSCMId.push($(this).attr("data_id1"));
        });
        var data = {
            StudentClassId: SCID,
            StudentSubjectId: inputID,
            Marks: inputValue,
            MarkId: inputHSSCMId,
            ExampTypeId: StudentExamTypeId
        }

        listclasses.push(data);
    });
    $.ajax({
        type: 'POST',
        url: 'StudentsSubject/Save',
        dataType: 'json',
        contentType: 'application/json',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },

        data: JSON.stringify({ ScoreModel: listclasses }),

        success: function (data) {

            clean.widget.success("ریکارد شما ثبت شد")

        },
        error: function (error) {
        }
    });
});
//=====================================End ==============================================================//

//===================================Save Student Upgration======================================================================//
$('#uxSave').on('click', function () {
    var StudentClassType = $('#uxclasstypeid').val();
    var StudentClassManagement = $('#uxclassmanagementid').val();
    var StudentClassID = $('#uxStudentDetails').find('tr');

    var ListStudents = [];
    var ListFinalData = [];
    debugger;
    StudentClassID.each(function () {
        var studentprofileid = $(this).attr("data");
        ListStudents.push(studentprofileid);
    });
    var finaldata = {

        ProfileID: ListStudents,
        ClassManagementID: StudentClassManagement,
        ClassTypeId: StudentClassType
    }
    ListFinalData.push(finaldata);
    let isConfirm = window.confirm("آیا مطمیین هستد که شاگردان شامل لیست را ارتقاع دهید؟");
    if (isConfirm == true) {
        if (StudentClassType > 12) {

            clean.widget.error("شما نمی توانید از صنف دوازده به با لا اتقاع دهید!")
        }
        else {
            debugger;
            $.ajax({
                type: 'POST',
                url: 'StudentResultPage/Save',
                dataType: 'json',
                contentType: 'application/json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },

                data: JSON.stringify({ ClassUpgrade: ListFinalData }),

                success: function (data) {

                    clean.widget.success("شاگردان انتخاب شده به صنف مد نظر ارتقاع یافت!")

                },
                error: function (error) {
                    clean.widget.error("ریکارد شما ثبت نشد.")
                }
            });
        }
    }
    else {
        return null;
    }
});

// =====================================End of student upgration==============================================///


