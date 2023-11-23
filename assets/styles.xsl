<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <style>
          table {
            border-collapse: collapse;
            width: 100%;
          }
          th, td {
            border: 1px solid black;
            padding: 8px;
            text-align: left;
          }
          th {
            background-color: #f2f2f2;
          }
        </style>
      </head>
      <body>
        <h2>Student Parliament Events</h2>
        <table>
          <tr>
            <th>Name</th>
            <th>Faculty</th>
            <th>Department</th>
            <th>Specialty</th>
            <th>Timing</th>
          </tr>
          <xsl:apply-templates select="students_parliament/event" />
        </table>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="event">
    <tr>
      <td><xsl:value-of select="name" /></td>
      <td><xsl:value-of select="faculty" /></td>
      <td><xsl:value-of select="department" /></td>
      <td><xsl:value-of select="specialty" /></td>
      <td><xsl:value-of select="timing" /></td>
    </tr>
  </xsl:template>

</xsl:stylesheet>