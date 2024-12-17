import Head from 'next/head';
import styled from 'styled-components';
import { useState } from 'react';

export default function Homepage() {
  const [pdfData, setPdfData] = useState<Uint8Array | null>(null);

  const test = async () => {
    try {
      const response = await fetch(`http://localhost:5210/api/Main/test`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const arrayBuffer = await response.arrayBuffer();
      return new Uint8Array(arrayBuffer);
    } catch (error) {
      console.error('API Hatası:', error);
      throw error;
    }
  };


  const downloadPdf = async () => {
    const fetchedPdfData = await test();
    setPdfData(fetchedPdfData);
    if (pdfData) {
      // Create a blob from the PDF data
      const blob = new Blob([pdfData], { type: 'application/pdf' });
      // Create a download link
      const link = document.createElement('a');
      link.href = URL.createObjectURL(blob);
      link.download = 'downloaded.pdf';
      // Simulate a click to trigger the download
      link.click();
      // Clean up the URL object
      URL.revokeObjectURL(link.href);
    } else {
      console.error('No PDF data to download.');
    }
  };

  return (
    <>
      <Head>
        <title>Homepage</title>
        <meta
          name="description"
          content="Tempor nostrud velit fugiat nostrud duis incididunt Lorem deserunt est tempor aute dolor ad elit."
        />
      </Head>
      <HomepageWrapper>
        <div id="pdf-viewer" style={{ width: '100%', height: '600px', overflow: 'auto' }}>
          {/* Optional: PDF Viewer can remain if needed */}
        </div>

        {/* Button to download PDF */}
        <button onClick={downloadPdf}>
          PDF İndir
        </button>
      </HomepageWrapper>
    </>
  );
}

const HomepageWrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 2rem;

  & > :last-child {
    margin-bottom: 15rem;
  }

  button {
    padding: 10px 20px;
    font-size: 1rem;
    margin-top: 1rem;
    cursor: pointer;
    background-color: #0070f3;
    color: white;
    border: none;
    border-radius: 5px;
  }

  button:hover {
    background-color: #005bb5;
  }

  button:disabled {
    background-color: #ccc;
    cursor: not-allowed;
  }

  p {
    font-size: 1.2rem;
    color: #333;
  }
`;
