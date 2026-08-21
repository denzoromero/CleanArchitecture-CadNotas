namespace CleanCadNotas.Helpers
{
    public class GenerateCSSExt
    {
        public static string GenerateCSS()
        {
            string? csscode = @" .logo {
            display: flex;
            align-items: center;
            justify-content: center; /* Center horizontally */
            flex: 1.5;
            padding: 5px;
        }

        .custom-container {
            display: flex;
            flex-direction: column;
            border: 1px solid black;
            flex: 3;
        
        }

        .custom-containerFooter {
            display: flex; 
        }


        .custom-row-sign {
            display: flex;
            flex-direction: column; /* Header on top, box below */
            width: 100%; /* Set a fixed width for each block */
            border: 1px solid black; /* Adds a border around each block */
      /*       box-sizing: border-box; /* Ensures padding and border don't exceed width */ */
            padding: 5px;
             height: 100px;
        }

        .header {
            font-weight: bold;
            /* margin-bottom: 5px; */
            text-align: center;
        }

     .empty-box {
             border: 1px solid black; 
            height: 80px; /* Height of the empty box */
            display: flex; /* Use flexbox to align the child elements */
            flex-direction: column; /* Stack child elements vertically */
            justify-content: center; /* Vertically center the content */
            align-items: center;
            position: relative;
        }

      .upper-right {
            position: absolute; /* Take the span out of the normal flow */
            top: 0; /* Align to the top */
            right: 5px; /* Add some spacing from the right edge */
            margin: 0; /* Ensure no default margins affect the position */
        }

    .custom-col-box {
            position: relative;
          flex: none; /* Makes each column take up an equal amount of space */
            padding-right: 15px; /* Adds padding to the right of each column */
            padding-left: 15px; /* Adds padding to the left of each column */
            /*   box-sizing: border-box; */ /* Ensures padding is included in the element's total width and height */
            /* padding: 5px; */
            border: 1px solid black;
        }

        .LVMHeader {
            border-bottom: 1px solid black;
            width: 100%;
            text-align: center;
            align-items: center;
        }

        .translation {
            font-style: italic;
            font-size: 12px;
        }

        .parentbox {
            display: flex;
            justify-content: space-between;
            border: 1px solid black;
            height: 70px;
        }

        .parent-head {
            margin-top: 20px;
        }

        .fontmain {
            font-weight: bold;
            font-size: 11px;
        }

        .fontmainFoot {
            font-weight: bold;
            font-size: 11px;
        }

        .first-div {
            height: 60%; /* Set the desired height */
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            border-bottom: 1px solid black;
            display: flex;
            align-items: center;
        }

        .second-div {
            height: 40%; /* Set the desired height */
            position: absolute;
            bottom: 0;
            left: 0;
            right: 0;
            align-items: center;
        }

        .flex-container {
            display: flex;
            flex-direction: column;
            flex: 1.5;
            border: 1px solid black;
            position: relative;
        }

        .sheet-container {
            display: flex;
            flex-direction: column;
            flex: 1;
            border: 1px solid black;
            position: relative;
        }

        .sheetheader {
            height: 20%; /* Set the desired height */
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            border-bottom: 1px solid black;
            display: flex;
            align-items: center;
            text-align: center;
            justify-content: center;
        }

        .sheetvalue {
            height: 80%; /* Set the desired height */
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            display: flex;
            align-items: center;
            text-align: center;
            justify-content: center;
        }

        .custom-row {
            display: flex;
            flex-wrap: wrap; /* Allows columns to wrap to the next line if necessary */
            /*margin-right: -15px;*/ /* Adjusts for padding in columns */
            /*margin-left: -15px;*/ /* Adjusts for padding in columns */
            /*margin-bottom: 15px;*/ /* Adds space between rows */
            border: 1px solid black;
        }

        .custom-col {
            flex: 1; /* Makes each column take up an equal amount of space */
            padding-right: 15px; /* Adds padding to the right of each column */
            padding-left: 15px; /* Adds padding to the left of each column */
            box-sizing: border-box; /* Ensures padding is included in the element's total width and height */
            padding: 5px;
            border: 1px solid black;
        }


        .custom-col2 {
            flex: 2; /* Makes each column take up an equal amount of space */
            padding-right: 15px; /* Adds padding to the right of each column */
            padding-left: 15px; /* Adds padding to the left of each column */
            box-sizing: border-box; /* Ensures padding is included in the element's total width and height */
            padding: 5px;
            border: 1px solid black;
        }

        .custom-col3 {
            flex: 3; /* Makes each column take up an equal amount of space */
            padding-right: 15px; /* Adds padding to the right of each column */
            padding-left: 15px; /* Adds padding to the left of each column */
            box-sizing: border-box; /* Ensures padding is included in the element's total width and height */
            padding: 5px;
            border: 1px solid black;
        }";
            return csscode;
        }
    }
}
