<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AssessmentProgress.ascx.vb" Inherits="ISAFIRRiskAssessmentWebApp.AssessmentProgress" %>

<div class="assessment-progress mb-4">
    <div class="progress-steps">
        <div class="step <%= If(CurrentStep = "facility", "active", If(StepCompleted("facility"), "completed", "")) %>">
            <div class="step-icon">
                <i class="fas fa-building"></i>
            </div>
            <div class="step-label">Facility</div>
        </div>
        <div class="step <%= If(CurrentStep = "threat", "active", If(StepCompleted("threat"), "completed", "")) %>">
            <div class="step-icon">
                <i class="fas fa-exclamation-triangle"></i>
            </div>
            <div class="step-label">Threats</div>
        </div>
        <div class="step <%= If(CurrentStep = "consequence", "active", If(StepCompleted("consequence"), "completed", "")) %>">
            <div class="step-icon">
                <i class="fas fa-chart-line"></i>
            </div>
            <div class="step-label">Consequences</div>
        </div>
        <div class="step <%= If(CurrentStep = "lifeline", "active", If(StepCompleted("lifeline"), "completed", "")) %>">
            <div class="step-icon">
                <i class="fas fa-link"></i>
            </div>
            <div class="step-label">Lifelines</div>
        </div>
        <div class="step <%= If(CurrentStep = "vulnerability", "active", If(StepCompleted("vulnerability"), "completed", "")) %>">
            <div class="step-icon">
                <i class="fas fa-shield-alt"></i>
            </div>
            <div class="step-label">Vulnerability</div>
        </div>
    </div>
</div>

<style>
.assessment-progress {
    padding: 20px;
    background: #f8f9fa;
    border-radius: 8px;
}

.progress-steps {
    display: flex;
    justify-content: space-between;
    position: relative;
}

.progress-steps::before {
    content: '';
    position: absolute;
    top: 20px;
    left: 0;
    right: 0;
    height: 2px;
    background: #dee2e6;
    z-index: 1;
}

.step {
    position: relative;
    z-index: 2;
    text-align: center;
    flex: 1;
}

.step-icon {
    width: 40px;
    height: 40px;
    background: #fff;
    border: 2px solid #dee2e6;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 auto 8px;
}

.step.active .step-icon {
    border-color: #3B1E54;
    background: #3B1E54;
    color: #fff;
}

.step.completed .step-icon {
    border-color: #28a745;
    background: #28a745;
    color: #fff;
}

.step-label {
    font-size: 14px;
    color: #6c757d;
}

.step.active .step-label {
    color: #3B1E54;
    font-weight: bold;
}

.step.completed .step-label {
    color: #28a745;
}
</style> 