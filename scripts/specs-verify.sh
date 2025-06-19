#!/bin/bash

PARAMS="{ \
    \"apiUrl\": \"http://localhost:8000/haalcentraal/api\", \
    \"logFileToAssert\": \"./test-data/logs/bewoning-data-service.json\", \
    \"oAuth\": { \
        \"enable\": false \
    } \
}"

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie-summary.txt \
                -f summary \
                features/docs \
                -p UnitTest \
                > /dev/null

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-integratie-summary.txt \
                -f summary \
                features/docs \
                -p Integratie \
                > /dev/null

verify() {
    npx cucumber-js -f json:./test-reports/cucumber-js/bewoning/test-result-$1.json \
                    -f summary:./test-reports/cucumber-js/bewoning/test-result-$1-summary.txt \
                    -f summary \
                    features/$1 \
                    --tags "not @skip-verify" \
                    --world-parameters "$PARAMS"
}

verify "raadpleeg-bewoning-met-periode"
verify "raadpleeg-bewoning-op-peildatum"
verify "geboortedatum"
verify "geslacht"
verify "naam"
verify "protocollering"