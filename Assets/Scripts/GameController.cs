using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
	
	private static float _velocity = 0;
	private static float _score =0;
	private static float _energy = 0;
	private static float _nextMilestone = 0;
	private static float _bgSoundTime = 0;
		
	//public GameController()
	//{
	//	myParameters = new Dictionary<string, float> ();
	//}

	public static void setArguments(float velocity, float score, float energy, float nextMilestone, float bgSoundTime)
	{
		_velocity = velocity;
		_score = score;
		_energy = energy;
		_nextMilestone = nextMilestone;
		_bgSoundTime = bgSoundTime;
	}

	public static void clearArguments()
	{
		_velocity = 0;
		_score = 0;
		_energy = 0;
		_nextMilestone = 0;
		_bgSoundTime = 0;
	}

	public static float getVelocity()
	{
		return _velocity;
	}

	public static float getScore()
	{
		return _score;
	}

	public static float getEnergy()
	{
		return _energy;
	}

	public static float getNextMilestone()
	{
		return _nextMilestone;
	}

	public static float getBgSoundTime()
	{
		return _bgSoundTime;
	}
}

