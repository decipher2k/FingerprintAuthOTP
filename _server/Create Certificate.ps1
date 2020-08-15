$dwp = @{
 Subject = 'CN=h2x.us';
 KeyLength = 2048;
 HashAlgorithm = 'SHA256';
 KeyUsage = 'DigitalSignature';
 KeyExportPolicy = 'Exportable';
 KeySpec = 'Signature';
 NotAfter = (Get-Date).AddYears(2) ;
 TextExtension = '2.5.29.37={text}1.3.6.1.5.5.7.3.3'
}
New-SelfSignedCertificate @dwp


$dwp = @{
 Subject = 'info@der-windows-papst.de';
 KeyLength = 2048; 
 KeySpec = 'KeyExchange';
 HashAlgorithm = 'SHA256';
 KeyExportPolicy = 'Exportable';
 KeyUsage = 'KeyEncipherment','DataEncipherment' ;
 NotAfter = (Get-Date).AddYears(2);
 TextExtension = '2.5.29.37={text}1.3.6.1.4.1.311.80.1';
}
New-SelfSignedCertificate @dwp
