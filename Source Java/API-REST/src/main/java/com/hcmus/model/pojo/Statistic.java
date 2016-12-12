package com.hcmus.model.pojo;

import java.math.BigDecimal;
import java.util.Date;

public class Statistic {
	private String name;
	private BigDecimal value;
	//private BigDecimal returnAmount;
	
	public Statistic () {}
	
	public Statistic (String name, BigDecimal value) {
		this.name = name;
		this.value = value;
		//this.returnAmount = returnAmount;
	}
	
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public BigDecimal getValue() {
		return value;
	}
	public void setValue(BigDecimal value) {
		this.value = value;
	}
	/*public BigDecimal getReturnAmount() {
		return returnAmount;
	}
	public void setReturnAmount(BigDecimal returnAmount) {
		this.returnAmount = returnAmount;
	}*/
	
}
