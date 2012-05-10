function isUndefined(x) { var u; return x === u; }

jQuery.noConflict();
var $j = jQuery;
$j(function () {
    hideValues();

    $j('[id$="verifyAttendee"]').click(function () {
        hideValues();
    });
});

function hideValues() {
    if ($j('[id$="depositCostAmount"]').html().split('$')[1] < 1) {
        $j('[id$="depositCost"]').hide();
        $j('[id$="depositCostAmount"]').hide();
        $j('[for$="depositCost"]').hide();
        $j('#Goal1Date').hide();
        $j('[id$="contGoal1Date"]').hide();
    }
    if ($j('[id$="registrationDeadlineDeposit2Amount"]').html().split('$')[1] < 1) {
        $j('[id$="registrationDeadlineDeposit2Cost"]').hide();
        $j('[id$="registrationDeadlineDeposit2Amount"]').hide();
        $j('[for$="registrationDeadlineDeposit2Cost"]').hide();
        $j('#Goal2Date').hide();
        $j('[id$="contGoal2Date"]').hide();
    }
    if ($j('[id$="registrationDeadlineDeposit3Amount"]').html().split('$')[1] < 1) {
        $j('[id$="registrationDeadlineDeposit3Cost"]').hide();
        $j('[id$="registrationDeadlineDeposit3Amount"]').hide();
        $j('[for$="registrationDeadlineDeposit3Cost"]').hide();
        $j('#Goal3Date').hide();
        $j('[id$="contGoal3Date"]').hide();
    }
    if ($j('[id$="registrationCostAmount"]').html().split('$')[1] < 1) {
        $j('[id$="registrationCost"]').hide();
        $j('[id$="registrationCostAmount"]').hide();
        $j('[for$="registrationCost"]').hide();
    }
    if ($j('[id$="balanceDueAmount"]').html().split('$')[1] < 1) {
        $j('[id$="balanceDue"]').hide();
        $j('[id$="balanceDueAmount"]').hide();
        $j('[for$="balanceDue"]').hide();
    }
}