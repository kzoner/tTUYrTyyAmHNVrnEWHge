// check/uncheck all checkbox
function checkUncheckAll(theForm, theElement, exceptcb) {
    var z = 0;
    for (z = 0; z < theForm.length; z++) {
        if (theForm[z].type == 'checkbox' && theForm[z].name != exceptcb) {
            theForm[z].checked = theElement.checked;
        }
    }
}

function removeChar(str, ch) {
    regExp = new RegExp("[" + ch + "]", "g");
    return str.replace(regExp, "");
}

// returns true if the string is empty
function isEmpty(s) {
    return (s == null || s.length == 0); //false isnot empty
}
// returns true if the string only contains characters A-Z or a-z
function isAlpha(str) {
    var re = /[^a-zA-Z]/g
    if (re.test(str)) return false;
    return true;
}
// returns true if the string only contains characters 0-9
function isNumber(s) {
    var r = /^\d+$/; return r.test(s);
}
// returns true if the string only contains characters A-Z, a-z or 0-9
function isAlphanumeric(s) {
    var r = /^[a-zA-Z0-9]+$/; return r.test(s);
}

/**********************************************************************
	IN:
		NUM - the number to format
		decimalNum - the number of decimal places to format the number to
		bolLeadingZero - true / false - display a leading zero for
										numbers between -1 and 1
		bolParens - true / false - use parenthesis around negative numbers
		bolCommas - put commas as number separators.
 
	RETVAL:
		The formatted number!
 **********************************************************************/
function FormatNumber(num, decimalNum, bolLeadingZero, bolParens, bolCommas) {
    if (isNaN(parseInt(num))) return "NaN";

    var tmpNum = num;
    var iSign = num < 0 ? -1 : 1;		// Get sign of number

    // Adjust number so only the specified number of numbers after
    // the decimal point are shown.
    tmpNum *= Math.pow(10, decimalNum);
    tmpNum = Math.round(Math.abs(tmpNum))
    tmpNum /= Math.pow(10, decimalNum);
    tmpNum *= iSign;					// Readjust for sign


    // Create a string object to do our formatting on
    var tmpNumStr = new String(tmpNum);

    // See if we need to strip out the leading zero or not.
    if (!bolLeadingZero && num < 1 && num > -1 && num != 0)
        if (num > 0)
            tmpNumStr = tmpNumStr.substring(1, tmpNumStr.length);
        else
            tmpNumStr = "-" + tmpNumStr.substring(2, tmpNumStr.length);

    // See if we need to put in the commas
    if (bolCommas && (num >= 1000 || num <= -1000)) {
        var iStart = tmpNumStr.indexOf(".");
        if (iStart < 0)
            iStart = tmpNumStr.length;

        iStart -= 3;
        while (iStart >= 1) {
            tmpNumStr = tmpNumStr.substring(0, iStart) + "," + tmpNumStr.substring(iStart, tmpNumStr.length)
            iStart -= 3;
        }
    }

    // See if we need to use parenthesis
    if (bolParens && num < 0)
        tmpNumStr = "(" + tmpNumStr.substring(1, tmpNumStr.length) + ")";

    return tmpNumStr;		// Return our formatted string!
}

function ReplaceAll(iStr, v1, v2) {
    var i = 0, oStr = '', j = v1.length;
    while (i < iStr.length) {
        if (iStr.substr(i, j) == v1) {
            oStr += v2;
            i += j
        }
        else {
            oStr += iStr.charAt(i);
            i++;
        }
    }
    return oStr;
}

// Format number by using 1000 seperator
function FormatN(num) {
    var n = sTrim(ReplaceAll(num, ',', ''));
    if (n == '') return '0';
    var no = FormatNumber(parseInt(n), true, false, false, true);
    return no;
}

// returns true if the string's length equals "len"
function isLength(str, len) {
    return str.length == len;
}
// returns true if the string's length is between "min" and "max"
function isLengthBetween(str, min, max) {
    return (str.length >= min) && (str.length <= max);
}
// returns true if the string is a valid date formatted as...
// mm dd yyyy, mm/dd/yyyy, mm.dd.yyyy, mm-dd-yyyy
function isDate(str) {
    var re = /^(\d{1,2})[\s\.\/-](\d{1,2})[\s\.\/-](\d{4})$/
    if (!re.test(str)) return false;
    var result = str.match(re);
    var m = parseInt(result[1]);
    var d = parseInt(result[2]);
    var y = parseInt(result[3]);
    if (m < 1 || m > 12 || y < 1900 || y > 2100) return false;
    if (m == 2) {
        var days = ((y % 4) == 0) ? 29 : 28;
    } else if (m == 4 || m == 6 || m == 9 || m == 11) {
        var days = 30;
    } else {
        var days = 31;
    }
    return (d >= 1 && d <= days);
}
/*-------------------longndh------------------------*/
function isDateDDMMYYYY(str) {
    var re = /^(\d{1,2})[\s\.\/-](\d{1,2})[\s\.\/-](\d{4})$/
    if (!re.test(str)) return false;
    var result = str.match(re);
    var m = parseInt(result[2]);
    var d = parseInt(result[1]);
    var y = parseInt(result[3]);
    if (m < 1 || m > 12 || y < 1900 || y > 2100) return false;
    if (m == 2) {
        var days = ((y % 4) == 0) ? 29 : 28;
    } else if (m == 4 || m == 6 || m == 9 || m == 11) {
        var days = 30;
    } else {
        var days = 31;
    }
    return (d >= 1 && d <= days);
}
/*-------------------------------------------*/

// returns true if "str1" is the same as the "str2"
function isMatch(str1, str2) {
    return str1 == str2;
}
// returns true if the string contains only whitespace
// cannot check a password type input for whitespace
function isWhitespace(str) { // NOT USED IN FORM VALIDATION
    var re = /[\S]/g
    if (re.test(str)) return false;
    return true;
}
// removes any whitespace from the string and returns the result
// the value of "replacement" will be used to replace the whitespace (optional)
function stripWhitespace(str, replacement) {// NOT USED IN FORM VALIDATION
    if (replacement == null) replacement = '';
    var result = str;
    var re = /\s/g
    if (str.search(re) != -1) {
        result = str.replace(re, replacement);
    }
    return result;
}
//
function identify_Browser() {
    d = document;
    this.agt = navigator.userAgent.toLowerCase();
    this.major = parseInt(navigator.appVersion);
    this.dom = (d.getElementById);
    this.ns = (d.layers);
    this.ns4up = (this.ns && this.major >= 4);
    this.ns6 = (this.dom && navigator.appName == "Netscape");
    this.op = (window.opera);
    if (d.all) this.ie = 1; else this.ie = 0;
    this.ie4 = (d.all && !this.dom);
    this.ie4up = (this.ie && this.major >= 4);
    this.ie5 = (d.all && this.dom);
    this.ie6 = (d.nodeType);
    this.sf = (this.agt.indexOf("safari") != -1);
    this.win = ((this.agt.indexOf("win") != -1) || (this.agt.indexOf("16bit") != -1));
    this.winme = (this.agt.indexOf("win 9x 4.90") != -1);
    this.xpsp2 = (this.agt.indexOf("sv1") != -1);
    this.mac = (this.agt.indexOf("mac") != -1);
}
var oBw = new identify_Browser();

function collapse(id) {
    if (oBw.ie5 || oBw.ns6) {  //Disable function in unsupported browsers
        Istate = document.getElementById(id).style.display;
        if (Istate == 'none') {
            Istate = 'block';
        }
        else {
            Istate = 'none';
        }
        document.getElementById(id).style.display = Istate;
    }
}
// Ham kiem tra so
function CheckNumeric(e) {
    var key //= (window.event) ? event.keyCode : e.which;   
    if (window.event) {
        key = event.keyCode
    }
    else {
        key = e.which
    }
    if (key > 47 && key < 58 || key == 8) {
        return
    }
    else if (window.event) //IE       
    {
        window.event.returnValue = null
    }
    else //Firefox       
    {
        e.preventDefault()
    }
}

// Hàm chặn phím enter
// Các dùng: bắt sự kiện onkeypress = "return preventEnter(event)"
function preventEnter(e) {
    var key //= (window.event) ? event.keyCode : e.which;   
    if (window.event) {
        key = event.keyCode
    }
    else {
        key = e.which
    }

    if (key != 13) {
        return
    }
    else if (window.event) //IE       
    {
        window.event.returnValue = null
    }
    else //Firefox       
    {
        e.preventDefault()
    }
}

// Trả về giá trị True nếu địa chỉ Email hợp lệ
function isValidEmail(str) {
    return str.match(/^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/)
}

// Hàm cắt khoảng trắng 2 ở đầu chuỗi
function sTrim(s) {
    while (s.charCodeAt(0) <= 32) {
        s = s.substr(1)
    }
    while (s.charCodeAt(s.length - 1) <= 32) {
        s = s.substr(0, s.length - 1)
    }
    return s
}

// Hàm kiểm tra ngày hợp lệ
function isDate(day, month, year) {
    if ((month < 1) || (month > 12)) return false
    var dt = new Date(year, month - 1, day)
    if (dt.getDay() != day)
        return false
    else
        return true
}

/////////////////////////////////////////////////////////////////////////////////////////////////
// ALL ABOUT AJAX HERE
/////////////////////////////////////////////////////////////////////////////////////////////////

// XmlHttp object class
function XmlHttp() {
    this.array = new Array(1);

    this.setValue = function (v) { this.array[0] = v; }
    this.getValue = function () { return this.array[0]; }
}

function loadXMLDoc(xmlHttp, url, callback, content) {
    xmlHttp.setValue(createXMLHttpRequest())
    if (xmlHttp.getValue()) {
        xmlHttp.getValue().onreadystatechange = function () { eval(callback) }
        xmlHttp.getValue().open('POST', url, true)
        xmlHttp.getValue().setRequestHeader("Content-Type", "application/x-www-form-urlencoded")
        xmlHttp.getValue().send(content)
        return true
    }
    else {
        alert('Trình duyệt của bạn không hỗ trợ AJAX!\nBạn hãy sử dụng trình duyệt IE 4.0 trở lên hoặc Mozilla FireFox 1.0 trở lên')
        return false
    }
}

function createXMLHttpRequest() {
    var xmlHttp = null
    try {	// Firefox, Opera 8.0+, Safari
        xmlHttp = new XMLHttpRequest()
    }
    catch (e) {	// Internet Explorer
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP")
        }
        catch (e) {
            try {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP")
            }
            catch (e) {
                return null
            }
        }
    }
    return xmlHttp
}


function setHeader(s) {
    document.write("<div id='idHeaderPane' class='SetHeader' nowrap>" + s + "</div>");
}