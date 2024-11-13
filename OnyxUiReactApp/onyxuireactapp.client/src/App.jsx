import { useState } from 'react';
import './App.css';

function App() {

    const [countryCode, setCountryCode] = useState('');
    const [vatId, setVatId] = useState('');
    const [result, setResult] = useState('');
    const [error, setError] = useState('');


    const handleSubmit = async (e) => {
        e.preventDefault();

        // Input validation
        if (!countryCode.trim()) {
            setError('Country code is required.');
            setResult('');
            return;
        }

        if (!vatId.trim()) {
            setError('VAT ID is required.');
            setResult('');
            return;
        }

        setError('');

        try {
            const response = await fetch(`https://localhost:7284/verify-vat?countryCode=${countryCode}&vatId=${vatId}`, {
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            });

            const data = await response.json();

            if (response.ok) {
                if (data.status === "Valid") {
                    setResult(`VAT is VALID`);
                } else if (data.status === "Invalid") {
                    setError(`VAT is INVALID`);
                    setResult('');
                }
            } else {
                setError(`Validation Failed: ${data.status}`);
            }
        } catch {
            setError('Error connecting to the API.');
        }
    };

    return (
        <div className="App">
            <h1>EU VAT Validator</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Country Code:</label>
                    <input
                        type="text"
                        value={countryCode}
                        onChange={(e) => setCountryCode(e.target.value)}
                    />
                </div>
                <div>
                    <label>VAT ID:</label>
                    <input
                        type="text"
                        value={vatId}
                        onChange={(e) => setVatId(e.target.value)}
                    />
                </div>
                <button type="submit">Validate</button>
            </form>

            {error && <p className="error">{error}</p>}
            {result && <p className="result">{result}</p>}
        </div>
    );
}


export default App;