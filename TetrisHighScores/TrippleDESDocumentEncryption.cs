// **************************************************
// Code taken from the msdn article EncryptedXml Class.
//  http://msdn.microsoft.com/en-gb/library/system.security.cryptography.xml.encryptedxml(v=vs.80).aspx - .NET Framework 2
//  http://msdn.microsoft.com/en-gb/library/system.security.cryptography.xml.encryptedxml(v=vs.100).aspx - .NET Framework 4
// **************************************************

// ****************************************************
//
//  A simple utility class that uses the TripleDES algorithm to encrypt an XML document.
//
// ****************************************************

using System;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace TetrisHighScores
{
    class TrippleDESDocumentEncryption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrippleDESDocumentEncryption"/> class.
        /// </summary>
        public TrippleDESDocumentEncryption()
        {
        }

        /// <summary>
        /// Encrypts the specified doc.
        /// </summary>
        /// <param name="Doc">The doc.</param>
        /// <param name="ElementToEncrypt">The element to encrypt.</param>
        /// <param name="Alg">The alg.</param>
        /// <param name="KeyName">Name of the key.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Doc
        /// or
        /// ElementToEncrypt
        /// or
        /// Alg
        /// </exception>
        /// <exception cref="System.Xml.XmlException">The specified element was not found</exception>
        public static void Encrypt(XmlDocument Doc, string ElementToEncrypt, RSA Alg, string KeyName)
        {
            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (ElementToEncrypt == null)
                throw new ArgumentNullException("ElementToEncrypt");
            if (Alg == null)
                throw new ArgumentNullException("Alg");

            ////////////////////////////////////////////////
            // Find the specified element in the XmlDocument
            // object and create a new XmlElemnt object.
            ////////////////////////////////////////////////

            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementToEncrypt)[0] as XmlElement;

            // Throw an XmlException if the element was not found.
            if (elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");

            }

            //////////////////////////////////////////////////
            // Create a new instance of the EncryptedXml class 
            // and use it to encrypt the XmlElement with the 
            // a new random symmetric key.
            //////////////////////////////////////////////////

            // Create a 256 bit Rijndael key.
            RijndaelManaged sessionKey = new RijndaelManaged();
            sessionKey.KeySize = 256;

            EncryptedXml eXml = new EncryptedXml();

            byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, sessionKey, false);

            ////////////////////////////////////////////////
            // Construct an EncryptedData object and populate
            // it with the desired encryption information.
            ////////////////////////////////////////////////


            EncryptedData edElement = new EncryptedData();
            edElement.Type = EncryptedXml.XmlEncElementUrl;

            // Create an EncryptionMethod element so that the 
            // receiver knows which algorithm to use for decryption.

            edElement.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES256Url);

            // Encrypt the session key and add it to an EncryptedKey element.
            EncryptedKey ek = new EncryptedKey();

            byte[] encryptedKey = EncryptedXml.EncryptKey(sessionKey.Key, Alg, false);

            ek.CipherData = new CipherData(encryptedKey);

            ek.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncRSA15Url);

            // Set the KeyInfo element to specify the
            // name of the RSA key.

            // Create a new KeyInfo element.
            edElement.KeyInfo = new KeyInfo();

            // Create a new KeyInfoName element.
            KeyInfoName kin = new KeyInfoName();

            // Specify a name for the key.
            kin.Value = KeyName;

            // Add the KeyInfoName element to the 
            // EncryptedKey object.
            ek.KeyInfo.AddClause(kin);

            // Add the encrypted key to the 
            // EncryptedData object.

            edElement.KeyInfo.AddClause(new KeyInfoEncryptedKey(ek));

            // Add the encrypted element data to the 
            // EncryptedData object.
            edElement.CipherData.CipherValue = encryptedElement;

            ////////////////////////////////////////////////////
            // Replace the element from the original XmlDocument
            // object with the EncryptedData element.
            ////////////////////////////////////////////////////

            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);

        }

        /// <summary>
        /// Decrypts the specified doc.
        /// </summary>
        /// <param name="Doc">The doc.</param>
        /// <param name="Alg">The alg.</param>
        /// <param name="KeyName">Name of the key.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Doc
        /// or
        /// Alg
        /// or
        /// KeyName
        /// </exception>
        public static void Decrypt(XmlDocument Doc, RSA Alg, string KeyName)
        {
            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (Alg == null)
                throw new ArgumentNullException("Alg");
            if (KeyName == null)
                throw new ArgumentNullException("KeyName");

            // Create a new EncryptedXml object.
            EncryptedXml exml = new EncryptedXml(Doc);

            // Add a key-name mapping.
            // This method can only decrypt documents
            // that present the specified key name.
            exml.AddKeyNameMapping(KeyName, Alg);

            // Decrypt the element.
            exml.DecryptDocument();

        }
    }
}
