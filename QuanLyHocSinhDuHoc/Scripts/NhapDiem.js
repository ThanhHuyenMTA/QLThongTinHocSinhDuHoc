function NhapDiem(id_namhoc) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/KyHoc/ViewNhapDiem",
        dataType: "json",
        data: { id: id_namhoc },//dua ra id_namhoc
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#Diemhocki').css('display', 'block');
            var allInputs = document.getElementsByTagName("input");
            var results = [];
            for (var x = 0; x < allInputs.length; x++)
                allInputs[x].value='';
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}
function DiemKyI() {
    debugger;
    var diemkyI = {
        TenKiHoc: 'Hkỳ I',
        Toan: $('#ToanI').val(),
        Ly: $('#LyI').val(),
        Hoa: $('#HoaI').val(),
        Sinh: $('#SinhI').val(),
        Tin: $('#TinI').val(),
        Van: $('#VanI').val(),
        Su: $('#SuI').val(),
        Dia: $('#DiaI').val(),
        NgoaiNgu: $('#NgoaiNguI').val(),
        CongNghe: $('#CongNgheI').val(),
        QuocPhongAnNinh: $('#QuocPhongAnNinhI').val(),
        TheDuc: $('#TheDucI').val(),
        TuChon: $('#TuChonI').val(),
        GiaoDucCongDan: $('#GiaoDucCongDanI').val(),
        DiemTrungBinh: $('#DiemTrungBinhI').val()
    }

    $.ajax({
        type: "POST",
        url: "/KyHoc/NhapDiem",
        dataType: "json",
        data: JSON.stringify(diemkyI),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });

}
function DiemKyII() {
    var diemkyII = {
        TenKiHoc: 'Hkỳ II',
        Toan: $('#ToanII').val(),
        Ly: $('#LyII').val(),
        Hoa: $('#HoaII').val(),
        Sinh: $('#SinhII').val(),
        Tin: $('#TinII').val(),
        Van: $('#VanII').val(),
        Su: $('#SuII').val(),
        Dia: $('#DiaII').val(),
        NgoaiNgu: $('#NgoaiNguII').val(),
        CongNghe: $('#CongNgheII').val(),
        QuocPhongAnNinh: $('#QuocPhongAnNinhII').val(),
        TheDuc: $('#TheDucII').val(),
        TuChon: $('#TuChonII').val(),
        GiaoDucCongDan: $('#GiaoDucCongDanII').val(),
        DiemTrungBinh: $('#DiemTrungBinhII').val()
    }
    $.ajax({
        type: "POST",
        url: "/KyHoc/NhapDiem",
        dataType: "json",
        data: JSON.stringify(diemkyII),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });

}
function DiemKyCN() {
    var diemCN = {
        TenKiHoc: 'CN',
        Toan: $('#ToanCN').val(),
        Ly: $('#LyCN').val(),
        Hoa: $('#HoaCN').val(),
        Sinh: $('#SinhCN').val(),
        Tin: $('#TinCN').val(),
        Van: $('#VanCN').val(),
        Su: $('#SuCN').val(),
        Dia: $('#DiaCN').val(),
        NgoaiNgu: $('#NgoaiNguCN').val(),
        CongNghe: $('#CongNgheCN').val(),
        QuocPhongAnNinh: $('#QuocPhongAnNinhCN').val(),
        TheDuc: $('#TheDucCN').val(),
        TuChon: $('#TuChonCN').val(),
        GiaoDucCongDan: $('#GiaoDucCongDanCN').val(),
        DiemTrungBinh: $('#DiemTrungBinhCN').val()
    }
    $.ajax({
        type: "POST",
        url: "/KyHoc/NhapDiem",
        dataType: "json",
        data: JSON.stringify(diemCN),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });

}