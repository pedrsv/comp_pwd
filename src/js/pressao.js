const pressaoRanges = [
    { minSistolic: 0, maxSistolic: 119, minDiastolic: 0, maxDiastolic: 79, classification: "PA �tima", recommendation: "Continue mantendo um estilo de vida saud�vel." },
    { minSistolic: 120, maxSistolic: 129, minDiastolic: 80, maxDiastolic: 84, classification: "PA normal", recommendation: "�timo, continue monitorando regularmente." },
    { minSistolic: 130, maxSistolic: 139, minDiastolic: 85, maxDiastolic: 89, classification: "Pr�-hipertens�o", recommendation: "Aten��o, recomenda-se mudan�as no estilo de vida e monitoramento mais frequente." },
    { minSistolic: 140, maxSistolic: 159, minDiastolic: 90, maxDiastolic: 99, classification: "HA Est�gio 1", recommendation: "Consulte um m�dico para avalia��o e poss�vel tratamento." },
    { minSistolic: 160, maxSistolic: 179, minDiastolic: 100, maxDiastolic: 109, classification: "HA Est�gio 2", recommendation: "Situa��o cr�tica, procure atendimento m�dico imediatamente." },
    { minSistolic: 180, maxSistolic: Infinity, minDiastolic: 110, maxDiastolic: Infinity, classification: "HA Est�gio 3", recommendation: "Situa��o cr�tica, procurar atendimento de emerg�ncia imediatamente." }
];

function calculate() {
    const sistolic = parseInt(document.getElementById('sistolic').value);
    const diastolic = parseInt(document.getElementById('diastolic').value);
    const result = calculatePressao(sistolic, diastolic);
    document.getElementById('result').innerHTML = `PAM: ${result.PAM}<br>Classifica��o: ${result.Classification}<br>Recomenda��o: ${result.Recommendation}`;
}

function calculatePressao(sistolic, diastolic) {
    const pam = (sistolic + 2 * diastolic) / 3.0;
    let result = classifyPressao(sistolic, diastolic);
    return { PAM: pam.toFixed(2), ...result };
}

function classifyPressao(sistolic, diastolic) {
    const classification = pressaoRanges.find(rule => sistolic >= rule.minSistolic && sistolic <= rule.maxSistolic && diastolic >= rule.minDiastolic && diastolic <= rule.maxDiastolic);
    return classification ? 
        { Classification: classification.classification, Recommendation: classification.recommendation } : 
        { Classification: "Fora dos padr�es comuns", Recommendation: "Consulte um m�dico para avalia��o detalhada." };
}
