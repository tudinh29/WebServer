package com.hcmus.model.pojo;
// Generated Nov 29, 2016 3:35:54 PM by Hibernate Tools 3.5.0.Final

import java.util.HashSet;
import java.util.Set;

/**
 * CardType generated by hbm2java
 */
public class CardType implements java.io.Serializable{

	private String cardTypeCode;
	private String description;
	private Set<Card> cards = new HashSet<Card>(0);

	public CardType() {
	}

	public CardType(String cardTypeCode, String description) {
		this.cardTypeCode = cardTypeCode;
		this.description = description;
	}

	public CardType(String cardTypeCode, String description, Set<Card> cards) {
		this.cardTypeCode = cardTypeCode;
		this.description = description;
		this.cards = cards;
	}

	public String getCardTypeCode() {
		return this.cardTypeCode;
	}

	public void setCardTypeCode(String cardTypeCode) {
		this.cardTypeCode = cardTypeCode;
	}

	public String getDescription() {
		return this.description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public Set<Card> getCards() {
		return this.cards;
	}

	public void setCards(Set<Card> cards) {
		this.cards = ORMUtils.initializeAndUnproxy(cards);
	}

}