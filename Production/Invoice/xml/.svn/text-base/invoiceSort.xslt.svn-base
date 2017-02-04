<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
<xsl:template match="/">
<invoices>
<xsl:for-each select="invoices/invoice">
<invoice>
	<id><xsl:value-of select="id"/></id>
	<number><xsl:value-of select="number"/></number>
	<date><xsl:value-of select="date"/></date>
	<client_id><xsl:value-of select="client_id"/></client_id>
	<customer>
		<custid><xsl:value-of select="customer/custid"/></custid>
		<name><xsl:value-of select="customer/name"/></name>
		<address_one><xsl:value-of select="customer/address_one"/></address_one>
		<address_two><xsl:value-of select="customer/address_two"/></address_two>
		<city><xsl:value-of select="customer/city"/></city>
		<state><xsl:value-of select="customer/state"/></state>
		<zip><xsl:value-of select="customer/zip"/></zip>
		<country><xsl:value-of select="customer/country"/></country>
		<phone><xsl:value-of select="customer/phone"/></phone>
		<fax><xsl:value-of select="customer/fax"/></fax>
	</customer>
	<terms><xsl:value-of select="terms"/></terms>
	<type><xsl:value-of select="type"/></type>
	<approved><xsl:value-of select="approved"/></approved>
	<printed><xsl:value-of select="printed"/></printed>
	<exported><xsl:value-of select="exported"/></exported>
	<jra_job_num><xsl:value-of select="jra_job_num"/></jra_job_num>
	<client_job_num><xsl:value-of select="client_job_num"/></client_job_num>
	<po_num><xsl:value-of select="po_num"/></po_num>
	<attention><xsl:value-of select="attention"/></attention>
	<job_description><xsl:value-of select="job_description"/></job_description>
	<study_dates><xsl:value-of select="study_dates"/></study_dates>
	<xsl:for-each select="detail">
	<xsl:sort data-type="number" select="order" />
	<detail>
		<id><xsl:value-of select="id"/></id>
		<order><xsl:value-of select="order"/></order>
		<show><xsl:value-of select="show"/></show>
		<account><xsl:value-of select="account"/></account>
		<task><xsl:value-of select="task"/></task>
		<department><xsl:value-of select="department"/></department>
		<quantity><xsl:value-of select="quantity"/></quantity>
		<line_description><xsl:value-of select="line_description"/></line_description>
		<price><xsl:if test="boolean(number(price))"><xsl:value-of select="price"/></xsl:if></price>
	</detail>
	</xsl:for-each>
	<xsl:for-each select="deposit">
	<deposit>
		<deposit_amount><xsl:value-of select="deposit_amount"/></deposit_amount>
	</deposit>
	</xsl:for-each>
</invoice>
</xsl:for-each>
</invoices>
</xsl:template>
</xsl:stylesheet>

  