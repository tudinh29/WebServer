package com.hcmus.model.pojo;

import java.math.BigDecimal;
import java.util.Date;

public class MerchantSummaryDailyTiny implements java.io.Serializable {
	private Date reportDate;
	private String merchantCode;
	private BigDecimal saleAmount;
	private Integer saleCount;
	private BigDecimal returnAmount;
	private Integer returnCount;
	private BigDecimal netAmount;
	private Integer transactionCount;
	private BigDecimal keyedAmount;
	
	public MerchantSummaryDailyTiny () {}
	
	public MerchantSummaryDailyTiny (Date reportDate, String merchantCode, BigDecimal saleAmount, Integer saleCount,
			BigDecimal returnAmount, Integer returnCount, BigDecimal netAmount, Integer transactionCount,
			BigDecimal keyedAmount) {
		this.reportDate = reportDate;
		this.merchantCode = merchantCode;
		this.saleAmount = saleAmount;
		this.saleCount = saleCount;
		this.returnAmount = returnAmount;
		this.returnCount = returnCount;
		this.netAmount = netAmount;
		this.transactionCount = transactionCount;
		this.keyedAmount = keyedAmount;
	}
	
	public Date getReportDate() {
		return reportDate;
	}
	public void setReportDate(Date reportDate) {
		this.reportDate = reportDate;
	}
	public String getMerchantCode() {
		return merchantCode;
	}
	public void setMerchantCode(String merchantCode) {
		this.merchantCode = merchantCode;
	}
	public BigDecimal getSaleAmount() {
		return saleAmount;
	}
	public void setSaleAmount(BigDecimal saleAmount) {
		this.saleAmount = saleAmount;
	}
	public Integer getSaleCount() {
		return saleCount;
	}
	public void setSaleCount(Integer saleCount) {
		this.saleCount = saleCount;
	}
	public BigDecimal getReturnAmount() {
		return returnAmount;
	}
	public void setReturnAmount(BigDecimal returnAmount) {
		this.returnAmount = returnAmount;
	}
	public Integer getReturnCount() {
		return returnCount;
	}
	public void setReturnCount(Integer returnCount) {
		this.returnCount = returnCount;
	}
	public BigDecimal getNetAmount() {
		return netAmount;
	}
	public void setNetAmount(BigDecimal netAmount) {
		this.netAmount = netAmount;
	}
	public Integer getTransactionCount() {
		return transactionCount;
	}
	public void setTransactionCount(Integer transactionCount) {
		this.transactionCount = transactionCount;
	}
	public BigDecimal getKeyedAmount() {
		return keyedAmount;
	}
	public void setKeyedAmount(BigDecimal keyedAmount) {
		this.keyedAmount = keyedAmount;
	}
}
