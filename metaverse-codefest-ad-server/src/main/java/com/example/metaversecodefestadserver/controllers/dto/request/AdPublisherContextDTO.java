package com.example.metaversecodefestadserver.controllers.dto.request;

import lombok.Data;

@Data
public class AdPublisherContextDTO {

    private String publisherId;

    private String[] activities;
    private String ageRating;

}
