const report = require('multiple-cucumber-html-reporter');

const customData = {
    title: 'BRP API',
    data: [
        { label: 'Applicatie', value: 'Bewoning Data Service' },
        { label: 'Versie', value: `${process.argv[2]}` },
        { label: 'Build', value: `${process.argv[3]}`},
        { label: 'Branch', value: `${process.argv[4]}`}
    ]
};

report.generate({
    jsonDir: 'test-reports/cucumber-js/step-definitions',
    reportPath: 'test-reports/cucumber-js/reports/step-definitions',
    reportName: 'Stap definities tests',
    hideMetadata: true
});

report.generate({
    jsonDir: 'test-reports/cucumber-js/bewoning',
    reportPath: 'test-reports/cucumber-js/reports/bewoning/data-service',
    reportName: 'Bewoning data service features',
    hideMetadata: true,
    customData: customData
});