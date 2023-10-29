//set the cookie in the in the browser when it is clicked on
(function () {
    var button = document.querySelector("#cookieConsent button[data-cookie-string]");
    if (button != null) {
        button.addEventListener("click", function (event) {
            document.cookie = button.dataset.cookieString;
            var cookieContainer = document.querySelector("#cookieConsent");
            cookieContainer.remove();
        }, false);
    }
})();

//attatch a submit listener to the supervised model form
//then reformat the data and send a api request to /score
var supervisedButton = document.querySelector("#onnxForm");
if (supervisedButton != null) {

    supervisedButton.addEventListener("submit", function (event) {
        event.preventDefault();
        //reformat the data
        //console.log("In the ONNX FIle");

        const formData = new FormData(event.target);

        // get the values of the form elements
        const squarenorthsouth = formData.get("squarenorthsouth");
        const headdirection = formData.get("headdirection");
        const sex = formData.get("sex");
        const northsouth = formData.get("northsouth");
        const eastwest = formData.get("eastwest");
        const adultsubadult = formData.get("adultsubadult");
        const preservation = formData.get("preservation");
        const squareeastwest = formData.get("squareeastwest");
        const area = formData.get("area");
        const ageatdeath = formData.get("ageatdeath");

        const jsonData = {
            "squarenorthsouth": parseFloat(squarenorthsouth),
            "squareeastwest": parseFloat(squareeastwest),
            "headdirection_E": 0.0,
            "headdirection_W": 0.0,
            "northsouth_N": 0.0,
            "sex_F": 0.0,
            "sex_M": 0.0,
            "preservation_B": 0.0,
            "preservation_Other": 0.0,
            "preservation_S": 0.0,
            "preservation_Skull only": 0.0,
            "preservation_W": 0.0,
            "preservation_bones and skull": 0.0,
            "preservation_deteriorated": 0.0,
            "preservation_disturbed": 0.0,
            "preservation_fair": 0.0,
            "preservation_good": 0.0,
            "preservation_headless skeleton": 0.0,
            "preservation_poor": 0.0,
            "preservation_poorly preserved": 0.0,
            "preservation_very disturbed": 0.0,
            "preservation_wrapped - bones showing": 0.0,
            "eastwest_E": 0.0,
            "eastwest_W": 0.0,
            "adultsubadult_A": 0.0,
            "adultsubadult_C": 0.0,
            "area_NE": 0.0,
            "area_NNW": 0.0,
            "area_NW": 0.0,
            "area_SE": 0.0,
            "area_SW": 0.0,
            "ageatdeath_A": 0.0,
            "ageatdeath_C": 0.0,
            "ageatdeath_I": 0.0,
            "ageatdeath_N": 0.0
        };


        if (headdirection == "E") {
            jsonData["headdirection_E"] = 1.0;
        }
        if (headdirection == "W") {
            jsonData["headdirection_W"] = 1.0;
        }
        if (northsouth == "N") {
            jsonData["northsouth_N"] = 1.0;
        }
        if (northsouth == "S") {
            jsonData["northsouth_S"] = 1.0;
        }
        if (sex == "M") {
            jsonData["sex_M"] = 1.0;
        }
        if (sex == "F") {
            jsonData["sex_F"] = 1.0;
        }
        if (preservation == "B") {
            jsonData["preservation_B"] = 1.0;
        }
        if (preservation == "O") {
            jsonData["preservation_Other"] = 1.0;
        }



        if (preservation == "S") {
            jsonData["preservation_S"] = 1.0;
        }
        if (preservation == "Skull") {
            jsonData["preservation_Skull only"] = 1.0;
        }
        if (preservation == "W") {
            jsonData["preservation_W"] = 1.0;
        }
        if (preservation == "BS") {
            jsonData["preservation_bones and skull"] = 1.0;
        }
        if (preservation == "Det") {
            jsonData["preservation_deteriorated"] = 1.0;
        }
        if (preservation == "Dis") {
            jsonData["preservation_disturbed"] = 1.0;
        }
        if (preservation == "F") {
            jsonData["preservation_fair"] = 1.0;
        }
        if (preservation == "G") {
            jsonData["preservation_good"] = 1.0;
        }
        if (preservation == "HDS") {
            jsonData["preservation_headless skeleton"] = 1.0;
        }
        if (preservation == "P") {
            jsonData["preservation_poor"] = 1.0;
        }
        if (preservation == "PP") {
            jsonData["preservation_poorly preserved"] = 1.0;
        }
        if (preservation == "VD") {
            jsonData["preservation_very disturbed"] = 1.0;
        }
        if (preservation == "WBS") {
            jsonData["preservation_wrapped - bones showing"] = 1.0;
        }
        if (eastwest == "E") {
            jsonData["eastwest_E"] = 1.0;
        }
        if (eastwest == "W") {
            jsonData["eastwest_W"] = 1.0;
        }
        if (adultsubadult == "A") {
            jsonData["adultsubadult_A"] = 1.0;
        }
        if (adultsubadult == "C") {
            jsonData["adultsubadult_C"] = 1.0;
        }
        if (area == "NE") {
            jsonData["area_NE"] = 1.0;
        }
        if (area == "NNW") {
            jsonData["area_NNW"] = 1.0;
        }
        if (area == "NW") {
            jsonData["area_NW"] = 1.0;
        }
        if (area == "SE") {
            jsonData["area_SE"] = 1.0;
        }
        if (area == "SW") {
            jsonData["area_SW"] = 1.0;
        }
        if (ageatdeath == "A") {
            jsonData["ageatdeath_A"] = 1.0;
        }
        if (ageatdeath == "C") {
            jsonData["ageatdeath_C"] = 1.0;
        }
        if (ageatdeath == "I") {
            jsonData["ageatdeath_I"] = 1.0;
        }

        fetch('/score', {
            method: 'POST',
            body: JSON.stringify(jsonData),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.json())
            .then(prediction => {
                //console.log(prediction);
                // Update the h4 tag with the prediction result
                var predictionResult = document.querySelector('#predictionResult')
                predictionResult.textContent = String("Predicted Wrapping: " + prediction.predictedValue);
                predictionResult.classList.add("show");
                predictionResult.scrollIntoView();
                //console.log("it got here at least");
            })
            .catch(error => {
                alert("Something went wrong...");
            });

    })
}

//function Onnx(event) {
    
//}