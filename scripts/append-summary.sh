#!/bin/bash

TEST_RESULTS_FILE="./test-reports/cucumber-js/bewoning/test-result-summary.txt"

{
  echo "### Cucumber Test Results"
  echo "```"
  cat "$TEST_RESULTS_FILE"
  echo "```"
} >> "$GITHUB_STEP_SUMMARY"