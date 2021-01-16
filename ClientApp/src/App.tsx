
import { useState } from 'react';
import axios from 'axios'
import './App.css';

function App() {
  const [file, setFile] = useState<FileList | null>(null);

  const upload = async () => { 
    if(file === null) {
      return;
    }
    
    const formData = new FormData();
    formData.append('file', file[0]);
    try{
      const uploadRequest = await axios.request({
        url: '/upload',
        method: 'POST',
        data: formData
      });

      console.log(uploadRequest.data);
    }catch(e) {
      alert(JSON.stringify(e.response))
      alert(e);
    }
  }

  return (
    <div className="App">
      <header className="App-header">
        <input type="file" onChange={(e) => setFile(e.target.files)}></input>
        <button onClick={upload}>Upload</button>
      </header>
    </div>
  );
}

export default App;
