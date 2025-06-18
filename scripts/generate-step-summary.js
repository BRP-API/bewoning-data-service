const fs = require('fs');
const path = require('path');
const { processFile } = require('./process-cucumber-file');

const outputFile = path.join(__dirname, './../test-reports/cucumber-js/bewoning/step-summary.txt');
fs.writeFileSync(outputFile, '', 'utf8');

const fileMap = new Map([
    ["./../test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie-summary.txt", "docs (zonder integratie)"],
    ["./../test-reports/cucumber-js/bewoning/test-result-raadpleeg-bewoning-met-periode-summary.txt", "raadpleeg bewoning met periode"],
    ["./../test-reports/cucumber-js/bewoning/test-result-raadpleeg-bewoning-op-peildatum-summary.txt", "raadpleeg bewoning op peildatum"],
    ["./../test-reports/cucumber-js/bewoning/test-result-geboortedatum-summary.txt", "geboortedatum"],
    ["./../test-reports/cucumber-js/bewoning/test-result-geslacht-summary.txt", "geslacht"],
    ["./../test-reports/cucumber-js/bewoning/test-result-naam-summary.txt", "naam"],
    ["./../test-reports/cucumber-js/bewoning/test-result-protocollering-summary.txt", "protocollering"]
]);

fileMap.forEach((caption, filePath) => {
    processFile(path.join(__dirname, filePath), outputFile, caption);
});
